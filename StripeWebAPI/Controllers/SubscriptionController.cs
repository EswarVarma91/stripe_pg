using Microsoft.AspNetCore.Mvc;
using Stripe;
using StripeWebAPI.Services;

namespace StripeWebAPI.Controllers;

/// <summary>
/// Reads subscription state recorded from webhooks.
///
///   GET /api/subscription/status?customerId=cus_123
///   GET /api/subscription/status?userId=42
///   GET /api/subscription/status?subscriptionId=sub_123
///   GET /api/subscription/status?customerId=cus_123&refresh=true   (also re-reads Stripe)
///
/// -> 200 { status, plan, interval, priceId, renewsAt, cancelAtPeriodEnd, ... }
/// -> 404 when no subscription is known for that identifier.
/// </summary>
[ApiController]
[Route("api/subscription")]
public class SubscriptionController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly ISubscriptionStore _store;

    public SubscriptionController(IConfiguration configuration, ISubscriptionStore store)
    {
        _configuration = configuration;
        _store = store;
    }

    [HttpGet("status")]
    public async Task<ActionResult<object>> Status(
        [FromQuery] string? subscriptionId,
        [FromQuery] string? customerId,
        [FromQuery] string? userId,
        [FromQuery] bool refresh = false)
    {
        if (string.IsNullOrWhiteSpace(subscriptionId) &&
            string.IsNullOrWhiteSpace(customerId) &&
            string.IsNullOrWhiteSpace(userId))
        {
            return BadRequest(new { error = "Provide at least one of subscriptionId, customerId or userId." });
        }

        var record = _store.Find(subscriptionId, customerId, userId);
        if (record is null)
        {
            return NotFound(new { error = "No subscription is known for that identifier yet." });
        }

        if (refresh && !string.IsNullOrEmpty(record.SubscriptionId))
        {
            StripeConfiguration.ApiKey = _configuration["Stripe:SecretKey"] ?? "";

            try
            {
                var subscription = await new SubscriptionService().GetAsync(record.SubscriptionId);
                SubscriptionMapper.Apply(record, subscription);
                _store.Upsert(record);
            }
            catch (StripeException ex)
            {
                return StatusCode(StatusCodes.Status502BadGateway, new
                {
                    error = "Could not refresh the subscription from Stripe: " + ex.Message
                });
            }
        }

        return Ok(new
        {
            record.UserId,
            record.CustomerId,
            record.SubscriptionId,
            record.PriceId,
            record.Plan,
            record.Interval,
            record.Status,
            record.CancelAtPeriodEnd,
            record.Currency,
            record.Amount,
            renewsAt = record.RenewalDate,
            record.LastEventId,
            record.UpdatedAt
        });
    }
}
