using StripeWebAPI.Models;

namespace StripeWebAPI.Services;

/// <summary>
/// Storage seam for subscription state. The default implementation is a local JSON file
/// (see <see cref="JsonSubscriptionStore"/>); replace it with a SQL Server implementation
/// in Program.cs when you have a real table - nothing outside this interface changes.
/// </summary>
public interface ISubscriptionStore
{
    /// <summary>
    /// Finds a record by the first non-empty of <paramref name="subscriptionId"/>,
    /// <paramref name="customerId"/>, <paramref name="userId"/>. Returns null when unknown.
    /// </summary>
    SubscriptionRecord? Find(string? subscriptionId, string? customerId, string? userId);

    /// <summary>Inserts or replaces the record keyed by its subscription id.</summary>
    void Upsert(SubscriptionRecord record);

    /// <summary>True when this Stripe event id has already been applied (webhook retries are common).</summary>
    bool IsProcessed(string eventId);

    /// <summary>Records a Stripe event id as applied. Call only after the event was handled successfully.</summary>
    void MarkProcessed(string eventId);
}
