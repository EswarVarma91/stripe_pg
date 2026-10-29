using Stripe;
using StripeWebAPI.Models;

namespace StripeWebAPI.Services;

/// <summary>
/// Maps a Stripe <see cref="Subscription"/> onto a <see cref="SubscriptionRecord"/> so the
/// webhook and the status endpoint apply exactly the same rules.
/// </summary>
public static class SubscriptionMapper
{
    public static void Apply(SubscriptionRecord record, Subscription subscription)
    {
        if (!string.IsNullOrEmpty(subscription.Id))
        {
            record.SubscriptionId = subscription.Id;
        }

        if (!string.IsNullOrEmpty(subscription.CustomerId))
        {
            record.CustomerId = subscription.CustomerId;
        }

        var status = Text(subscription.Status);
        if (!string.IsNullOrEmpty(status))
        {
            record.Status = status;
        }

        record.CancelAtPeriodEnd = subscription.CancelAtPeriodEnd;

        var price = subscription.Items?.Data?.FirstOrDefault()?.Price;
        if (price is not null)
        {
            if (!string.IsNullOrEmpty(price.Id))
            {
                record.PriceId = price.Id;
            }

            if (!string.IsNullOrEmpty(price.Currency))
            {
                record.Currency = price.Currency.ToLowerInvariant();
            }

            if (price.UnitAmount.HasValue)
            {
                record.Amount = price.UnitAmount;
            }

            var interval = price.Recurring?.Interval;
            if (!string.IsNullOrEmpty(interval))
            {
                record.Interval = interval;
                if (string.IsNullOrEmpty(record.Plan))
                {
                    record.Plan = interval == "year" ? "yearly" : "monthly";
                }
            }
        }

        if (string.IsNullOrEmpty(record.UserId) && subscription.Metadata is not null
            && subscription.Metadata.TryGetValue("userId", out var userId)
            && !string.IsNullOrEmpty(userId))
        {
            record.UserId = userId;
        }

        record.UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Renders a Stripe field as text without depending on whether that field is typed as a
    /// string or as a generated enum in the Stripe.net version being compiled against.
    /// </summary>
    public static string Text(object? value) => value?.ToString() ?? "";
}
