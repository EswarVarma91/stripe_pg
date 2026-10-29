using Microsoft.AspNetCore.Mvc;
using Stripe;
using StripeWebAPI.Services;

namespace StripeWebAPI.Controllers;

/// <summary>
/// Opens the Stripe customer portal so a subscriber can cancel, resume or change their card.
/// Stripe must have a portal configured in the dashboard (Settings > Billing > Customer portal).
///
///   POST /api/portal
///   { "customerId": "cus_123", "returnUrl": "https://example.com/account" }
///   -> 200 { url }
/// </summary>
[ApiController]
[Route("api/portal")]
public class PortalController : ControllerBase
{
    private readonly IConfiguration _configuration;

    public PortalController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public class PortalRequest
    {
        /// <summary>Stripe customer id, cus_...</summary>
        public string? CustomerId { get; set; }

        /// <summary>Where to send the customer back to; defaults to the app origin.</summary>
        public string? ReturnUrl { get; set; }
    }

    [HttpPost]
    public async Task<ActionResult<object>> Post([FromBody] PortalRequest request)
    {
        if (request is null || string.IsNullOrWhiteSpace(request.CustomerId))
        {
            return BadRequest(new { error = "customerId is required." });
        }

        StripeConfiguration.ApiKey = _configuration["Stripe:SecretKey"] ?? "";

        var origin = AppUrls.Origin(Request, _configuration["Stripe:AppOrigin"]);
        var returnUrl = AppUrls.Absolute(origin, request.ReturnUrl, "/");

        try
        {
            var session = await new Stripe.BillingPortal.SessionService().CreateAsync(
                new Stripe.BillingPortal.SessionCreateOptions
                {
                    Customer = request.CustomerId,
                    ReturnUrl = returnUrl
                });

            return Ok(new { url = session.Url ?? "" });
        }
        catch (StripeException ex)
        {
            return StatusCode(StatusCodes.Status502BadGateway, new
            {
                error = "Stripe could not open the customer portal: " + ex.Message
            });
        }
    }
}
