namespace StripeWebAPI.Models;

/// <summary>
/// One subscriber, as recorded from Stripe webhooks (and refreshed on demand).
/// This is our source of truth for "is this customer subscribed, and on which plan".
/// </summary>
public class SubscriptionRecord
{
    /// <summary>Your application's user id, when the checkout session carried one.</summary>
    public string UserId { get; set; } = "";

    /// <summary>Stripe customer id, cus_...</summary>
    public string CustomerId { get; set; } = "";

    /// <summary>Stripe subscription id, sub_... (the primary key of the store).</summary>
    public string SubscriptionId { get; set; } = "";

    /// <summary>Recurring price the subscriber is on, price_...</summary>
    public string PriceId { get; set; } = "";

    /// <summary>monthly | yearly - from checkout metadata, else derived from the price interval.</summary>
    public string Plan { get; set; } = "";

    /// <summary>month | year - the Stripe recurring interval.</summary>
    public string Interval { get; set; } = "";

    /// <summary>active | past_due | canceled | trialing | incomplete ... as reported by Stripe.</summary>
    public string Status { get; set; } = "";

    /// <summary>True when the subscriber cancelled but the subscription still runs to the period end.</summary>
    public bool CancelAtPeriodEnd { get; set; }

    /// <summary>Next billing date, taken from invoice.paid (next_payment_attempt).</summary>
    public DateTime? RenewalDate { get; set; }

    /// <summary>Three-letter currency of the price, usd ...</summary>
    public string Currency { get; set; } = "";

    /// <summary>Unit amount in the currency's minor units (1000 = 10.00).</summary>
    public long? Amount { get; set; }

    /// <summary>Stripe event id that last changed this record (useful for tracing).</summary>
    public string LastEventId { get; set; } = "";

    /// <summary>UTC timestamp of the last update.</summary>
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
