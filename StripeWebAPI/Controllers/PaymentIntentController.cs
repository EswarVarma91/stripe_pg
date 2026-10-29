using Microsoft.AspNetCore.Mvc;
using Stripe;

namespace StripeWebAPI.Controllers;

/// <summary>
/// Creates a Stripe PaymentIntent for a one-off card payment - the non-subscription flow.
/// (Subscriptions go through <see cref="CheckoutController"/> instead.)
///
///   POST /api/payment-intent
///   { "amount": 1000, "currency": "usd", "description": "Order 42" }
///   -> 200 { clientSecret, paymentIntentId, amount, currency }
///
/// The client then confirms it with Stripe.js: stripe.confirmCardPayment(clientSecret),
/// or through the Payment Element initialised with { clientSecret }.
///
/// This replaces the in-page controller that lived in StripePG/Default2.aspx.cs; the
/// response keeps the same `clientSecret` field the existing front end already reads.
/// </summary>
[ApiController]
[Route("api/payment-intent")]
public class PaymentIntentController : ControllerBase
{
    private readonly IConfiguration _configuration;

    public PaymentIntentController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public class PaymentIntentRequest
    {
        /// <summary>Amount in the currency's minor units (1000 = 10.00). Required.</summary>
        public long Amount { get; set; }

        /// <summary>Three-letter ISO currency; defaults to the Stripe:Currency setting.</summary>
        public string? Currency { get; set; }

        /// <summary>Optional description, shown in the Stripe dashboard.</summary>
        public string? Description { get; set; }

        /// <summary>Optional existing Stripe customer, cus_...</summary>
        public string? CustomerId { get; set; }

        /// <summary>Optional email address to send the receipt to.</summary>
        public string? ReceiptEmail { get; set; }

        /// <summary>Optional free-form key/value data stored on the PaymentIntent.</summary>
        public Dictionary<string, string>? Metadata { get; set; }
    }

    public class PaymentIntentResponse
    {
        public string ClientSecret { get; set; } = "";
        public string PaymentIntentId { get; set; } = "";
        public long Amount { get; set; }
        public string Currency { get; set; } = "";
    }

    [HttpPost]
    public async Task<ActionResult<PaymentIntentResponse>> Post([FromBody] PaymentIntentRequest request)
    {
        if (request is null)
        {
            return BadRequest(new { error = "A JSON body is required." });
        }

        if (request.Amount <= 0)
        {
            return BadRequest(new { error = "amount must be a positive integer in the currency's minor units (1000 = 10.00)." });
        }

        if (request.Amount < 50)
        {
            return BadRequest(new { error = "amount is below Stripe's minimum charge of 50 minor units." });
        }

        var configuredCurrency = _configuration["Stripe:Currency"];
        var currency = (string.IsNullOrWhiteSpace(request.Currency) ? configuredCurrency : request.Currency) ?? "usd";
        currency = currency.Trim().ToLowerInvariant();

        if (currency.Length != 3 || !currency.All(char.IsLetter))
        {
            return BadRequest(new { error = "currency must be a three-letter ISO code, e.g. usd." });
        }

        StripeConfiguration.ApiKey = _configuration["Stripe:SecretKey"] ?? "";

        // NOTE: the amount comes from the caller. For anything price-sensitive, compute it
        // server-side from your own catalogue before calling this endpoint.
        var options = new PaymentIntentCreateOptions
        {
            Amount = request.Amount,
            Currency = currency,
            AutomaticPaymentMethods = new PaymentIntentAutomaticPaymentMethodsOptions
            {
                Enabled = true
            }
        };

        if (!string.IsNullOrWhiteSpace(request.Description))
        {
            options.Description = request.Description;
        }

        if (!string.IsNullOrWhiteSpace(request.CustomerId))
        {
            options.Customer = request.CustomerId;
        }

        if (!string.IsNullOrWhiteSpace(request.ReceiptEmail))
        {
            options.ReceiptEmail = request.ReceiptEmail;
        }

        if (request.Metadata is { Count: > 0 })
        {
            options.Metadata = request.Metadata;
        }

        try
        {
            var paymentIntent = await new PaymentIntentService().CreateAsync(options);

            return Ok(new PaymentIntentResponse
            {
                ClientSecret = paymentIntent.ClientSecret ?? "",
                PaymentIntentId = paymentIntent.Id ?? "",
                // The intent is created with exactly this amount, so echoing it is safe and
                // avoids depending on how Amount is typed in the pinned Stripe.net version.
                Amount = request.Amount,
                Currency = paymentIntent.Currency ?? ""
            });
        }
        catch (StripeException ex)
        {
            return StatusCode(StatusCodes.Status502BadGateway, new
            {
                error = "Stripe rejected the payment intent: " + ex.Message
            });
        }
    }
}
