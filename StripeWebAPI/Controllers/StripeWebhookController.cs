using Microsoft.AspNetCore.Mvc;
using Stripe;
using StripeWebAPI.Services;

namespace StripeWebAPI.Controllers;

/// <summary>
/// Stripe webhook receiver. Verifies the signature, applies subscription state to the store
/// and returns 2xx quickly. Events are de-duplicated by id because Stripe retries deliveries.
///
///   POST /api/StripeWebhook
///
/// Handled: checkout.session.completed, invoice.paid, invoice.payment_failed,
///          customer.subscription.updated, customer.subscription.deleted.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class StripeWebhookController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly ISubscriptionStore _store;
    private readonly ILogger<StripeWebhookController> _logger;

    public StripeWebhookController(
        IConfiguration configuration,
        ISubscriptionStore store,
        ILogger<StripeWebhookController> logger)
    {
        _configuration = configuration;
        _store = store;
        _logger = logger;
    }

    [HttpPost]
    public async Task<IActionResult> Post()
    {
        string json;
        using (var reader = new StreamReader(HttpContext.Request.Body))
        {
            json = await reader.ReadToEndAsync();
        }

        Stripe.Event stripeEvent;
        try
        {
            var signatureHeader = Request.Headers["Stripe-Signature"];
            var webhookSecret = _configuration["Stripe:WebhookSecret"] ?? "";
            stripeEvent = EventUtility.ConstructEvent(json, signatureHeader, webhookSecret, throwOnApiVersionMismatch: false);
        }
        catch (StripeException ex)
        {
            _logger.LogWarning(ex, "Rejected Stripe webhook: bad signature or unreadable payload");
            return BadRequest(new { error = "Invalid webhook signature." });
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Rejected Stripe webhook: could not parse event");
            return BadRequest(new { error = "Could not parse webhook event." });
        }

        // Stripe retries deliveries, so anything already applied is acknowledged without re-running.
        if (_store.IsProcessed(stripeEvent.Id))
        {
            return Ok();
        }

        try
        {
            await HandleAsync(stripeEvent);
            _store.MarkProcessed(stripeEvent.Id);
        }
        catch (Exception ex)
        {
            // A non-2xx answer makes Stripe retry, so a transient failure here is not lost.
            _logger.LogError(ex, "Failed to handle Stripe event {EventType} ({EventId})", stripeEvent.Type, stripeEvent.Id);
            return StatusCode(StatusCodes.Status500InternalServerError, new { error = "Event processing failed." });
        }

        return Ok();
    }

    private async Task HandleAsync(Stripe.Event stripeEvent)
    {
        switch (stripeEvent.Type)
        {
            case "checkout.session.completed":
            {
                var session = stripeEvent.Data.Object as Stripe.Checkout.Session;
                if (session is null || SubscriptionMapper.Text(session.Mode) != "subscription")
                {
                    return;
                }

                if (string.IsNullOrEmpty(session.SubscriptionId))
                {
                    return;
                }

                var record = _store.Find(session.SubscriptionId, null, null)
                             ?? new Models.SubscriptionRecord { SubscriptionId = session.SubscriptionId };

                if (!string.IsNullOrEmpty(session.CustomerId))
                {
                    record.CustomerId = session.CustomerId;
                }

                if (session.Metadata is not null)
                {
                    if (string.IsNullOrEmpty(record.UserId)
                        && session.Metadata.TryGetValue("userId", out var userId)
                        && !string.IsNullOrEmpty(userId))
                    {
                        record.UserId = userId;
                    }

                    if (session.Metadata.TryGetValue("plan", out var plan) && !string.IsNullOrEmpty(plan))
                    {
                        record.Plan = plan;
                    }

                    if (session.Metadata.TryGetValue("interval", out var interval) && !string.IsNullOrEmpty(interval))
                    {
                        record.Interval = interval;
                    }
                }

                record.LastEventId = stripeEvent.Id;

                try
                {
                    var subscription = await new SubscriptionService().GetAsync(session.SubscriptionId);
                    SubscriptionMapper.Apply(record, subscription);
                }
                catch (StripeException ex)
                {
                    // Still keep what we learned from the session; a later subscription event will fill the rest.
                    _logger.LogWarning(ex, "Could not read subscription {SubscriptionId} after checkout", session.SubscriptionId);
                }

                _store.Upsert(record);
                return;
            }

            case "invoice.paid":
            {
                var invoice = stripeEvent.Data.Object as Stripe.Invoice;
                if (invoice is null || string.IsNullOrEmpty(invoice.CustomerId))
                {
                    return;
                }

                var record = _store.Find(null, invoice.CustomerId, null);
                if (record is null)
                {
                    return;
                }

                if (invoice.NextPaymentAttempt.HasValue)
                {
                    record.RenewalDate = invoice.NextPaymentAttempt.Value.ToUniversalTime();
                }

                // Only promote to active from a state that means "not currently paid for".
                if (record.Status is "" or "past_due" or "unpaid" or "incomplete")
                {
                    record.Status = "active";
                }

                record.LastEventId = stripeEvent.Id;
                record.UpdatedAt = DateTime.UtcNow;
                _store.Upsert(record);
                return;
            }

            case "invoice.payment_failed":
            {
                var invoice = stripeEvent.Data.Object as Stripe.Invoice;
                if (invoice is null || string.IsNullOrEmpty(invoice.CustomerId))
                {
                    return;
                }

                var record = _store.Find(null, invoice.CustomerId, null);
                if (record is null)
                {
                    return;
                }

                record.Status = "past_due";
                record.LastEventId = stripeEvent.Id;
                record.UpdatedAt = DateTime.UtcNow;
                _store.Upsert(record);
                return;
            }

            case "customer.subscription.updated":
            case "customer.subscription.deleted":
            {
                var subscription = stripeEvent.Data.Object as Stripe.Subscription;
                if (subscription is null || string.IsNullOrEmpty(subscription.Id))
                {
                    return;
                }

                var record = _store.Find(subscription.Id, null, null)
                             ?? new Models.SubscriptionRecord { SubscriptionId = subscription.Id };

                SubscriptionMapper.Apply(record, subscription);

                if (stripeEvent.Type == "customer.subscription.deleted")
                {
                    record.Status = "canceled";
                }

                record.LastEventId = stripeEvent.Id;
                _store.Upsert(record);
                return;
            }

            default:
                // Other event types are acknowledged but not stored.
                return;
        }
    }
}
