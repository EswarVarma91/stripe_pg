using Microsoft.AspNetCore.Mvc;
using Stripe;
using Stripe.Checkout;
using StripeWebAPI.Services;

namespace StripeWebAPI.Controllers;

/// <summary>
/// Creates a Stripe Checkout session for a monthly or yearly subscription.
/// The client only ever names the plan - the price id and amount come from configuration,
/// so a client can never choose what it pays.
///
///   POST /api/checkout
///   { "plan": "monthly", "email": "a@b.com", "userId": "42" }
///   -> 200 { url, sessionId, plan, interval, priceId }
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class CheckoutController : ControllerBase
{
    private readonly IConfiguration _configuration;

    public CheckoutController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public class CheckoutRequest
    {
        /// <summary>"monthly" or "yearly".</summary>
        public string Plan { get; set; } = "";

        /// <summary>Optional pre-filled customer email.</summary>
        public string? Email { get; set; }

        /// <summary>Optional your-user-id; stored on the checkout session metadata and picked up by the webhook.</summary>
        public string? UserId { get; set; }

        /// <summary>Optional existing Stripe customer, cus_...</summary>
        public string? CustomerId { get; set; }

        /// <summary>Optional absolute or root-relative return URL on success.</summary>
        public string? SuccessUrl { get; set; }

        /// <summary>Optional absolute or root-relative return URL on cancel.</summary>
        public string? CancelUrl { get; set; }
    }

    public class CheckoutResponse
    {
        public string Url { get; set; } = "";
        public string SessionId { get; set; } = "";
        public string Plan { get; set; } = "";
        public string Interval { get; set; } = "";
        public string PriceId { get; set; } = "";
    }

    [HttpPost]
    public async Task<ActionResult<CheckoutResponse>> Post([FromBody] CheckoutRequest request)
    {
        if (request is null)
        {
            return BadRequest(new { error = "A JSON body is required." });
        }

        var plan = (request.Plan ?? "").Trim().ToLowerInvariant();
        if (plan is not ("monthly" or "yearly"))
        {
            return BadRequest(new { error = "plan must be 'monthly' or 'yearly'." });
        }

        var yearly = plan == "yearly";
        var priceKey = yearly ? "Stripe:YearlyPriceId" : "Stripe:MonthlyPriceId";
        var priceId = _configuration[priceKey] ?? "";

        if (string.IsNullOrWhiteSpace(priceId) || !priceId.StartsWith("price_", StringComparison.Ordinal))
        {
            return StatusCode(StatusCodes.Status503ServiceUnavailable, new
            {
                error = $"The {plan} plan is not configured. Set {priceKey} to a recurring price id from the Stripe Dashboard (Billing > Products)."
            });
        }

        StripeConfiguration.ApiKey = _configuration["Stripe:SecretKey"] ?? "";

        var origin = AppUrls.Origin(Request, _configuration["Stripe:AppOrigin"]);
        var successUrl = AppUrls.Absolute(origin,
            request.SuccessUrl ?? _configuration["Stripe:CheckoutSuccessUrl"],
            "/SubscriptionSuccess.aspx?session_id={CHECKOUT_SESSION_ID}");
        var cancelUrl = AppUrls.Absolute(origin,
            request.CancelUrl ?? _configuration["Stripe:CheckoutCancelUrl"],
            "/SubscriptionCancel.aspx");

        var options = new SessionCreateOptions
        {
            Mode = "subscription",
            LineItems = new List<SessionLineItemOptions>
            {
                new SessionLineItemOptions
                {
                    Price = priceId,
                    Quantity = 1
                }
            },
            SuccessUrl = successUrl,
            CancelUrl = cancelUrl,
            Metadata = new Dictionary<string, string>
            {
                { "plan", plan },
                { "interval", yearly ? "year" : "month" },
                { "userId", request.UserId ?? "" }
            }
        };

        if (!string.IsNullOrWhiteSpace(request.CustomerId))
        {
            options.Customer = request.CustomerId;
        }

        if (!string.IsNullOrWhiteSpace(request.Email))
        {
            options.CustomerEmail = request.Email;
        }

        try
        {
            var session = await new SessionService().CreateAsync(options);

            return Ok(new CheckoutResponse
            {
                Url = session.Url ?? "",
                SessionId = session.Id ?? "",
                Plan = plan,
                Interval = yearly ? "year" : "month",
                PriceId = priceId
            });
        }
        catch (StripeException ex)
        {
            return StatusCode(StatusCodes.Status502BadGateway, new
            {
                error = "Stripe rejected the checkout request: " + ex.Message
            });
        }
    }
}
