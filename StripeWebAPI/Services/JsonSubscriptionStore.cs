using System.Text.Json;
using StripeWebAPI.Models;

namespace StripeWebAPI.Services;

/// <summary>On-disk shape of <see cref="JsonSubscriptionStore"/>.</summary>
public class SubscriptionStoreData
{
    public Dictionary<string, SubscriptionRecord> Subscriptions { get; set; } = new();
    public List<string> ProcessedEventIds { get; set; } = new();
}

/// <summary>
/// JSON-file backed subscription store. Chosen because this solution has no database yet
/// and it needs no extra packages; writes are atomic (temp file + rename) and guarded by a
/// lock, which is enough for a single web process. Swap in a SQL implementation of
/// <see cref="ISubscriptionStore"/> when you have a table - no call site changes.
/// Data lives in {ContentRoot}/data/subscriptions.json (git-ignored).
/// </summary>
public class JsonSubscriptionStore : ISubscriptionStore
{
    private const int MaxProcessedEvents = 500;

    private static readonly JsonSerializerOptions SerializerOptions = new() { WriteIndented = true };

    private readonly string _path;
    private readonly object _gate = new();
    private SubscriptionStoreData? _data;

    public JsonSubscriptionStore(string path)
    {
        _path = path;
    }

    public SubscriptionRecord? Find(string? subscriptionId, string? customerId, string? userId)
    {
        lock (_gate)
        {
            var data = Load();

            if (!string.IsNullOrEmpty(subscriptionId))
                return FirstMatch(data, record => record.SubscriptionId, subscriptionId);

            if (!string.IsNullOrEmpty(customerId))
                return FirstMatch(data, record => record.CustomerId, customerId);

            if (!string.IsNullOrEmpty(userId))
                return FirstMatch(data, record => record.UserId, userId);

            return null;
        }
    }

    public void Upsert(SubscriptionRecord record)
    {
        if (string.IsNullOrEmpty(record.SubscriptionId))
        {
            return; // nothing to key the record on
        }

        lock (_gate)
        {
            var data = Load();
            data.Subscriptions[record.SubscriptionId] = record;
            Save(data);
        }
    }

    public bool IsProcessed(string eventId)
    {
        if (string.IsNullOrEmpty(eventId))
        {
            return false;
        }

        lock (_gate)
        {
            return Load().ProcessedEventIds.Contains(eventId);
        }
    }

    public void MarkProcessed(string eventId)
    {
        if (string.IsNullOrEmpty(eventId))
        {
            return;
        }

        lock (_gate)
        {
            var data = Load();
            if (data.ProcessedEventIds.Contains(eventId))
            {
                return;
            }

            data.ProcessedEventIds.Add(eventId);
            while (data.ProcessedEventIds.Count > MaxProcessedEvents)
            {
                data.ProcessedEventIds.RemoveAt(0);
            }

            Save(data);
        }
    }

    private static SubscriptionRecord? FirstMatch(
        SubscriptionStoreData data,
        Func<SubscriptionRecord, string> selector,
        string value)
    {
        foreach (var record in data.Subscriptions.Values)
        {
            if (string.Equals(selector(record), value, StringComparison.Ordinal))
            {
                return record;
            }
        }

        return null;
    }

    private SubscriptionStoreData Load()
    {
        if (_data is not null)
        {
            return _data;
        }

        if (!File.Exists(_path))
        {
            _data = new SubscriptionStoreData();
            return _data;
        }

        try
        {
            var json = File.ReadAllText(_path);
            _data = JsonSerializer.Deserialize<SubscriptionStoreData>(json, SerializerOptions)
                    ?? new SubscriptionStoreData();
        }
        catch (JsonException ex)
        {
            // Do NOT fall back to an empty store here: that would overwrite the file on the next save.
            throw new InvalidDataException(
                $"Subscription store '{_path}' could not be parsed. Fix or remove the file before continuing.", ex);
        }

        return _data;
    }

    private void Save(SubscriptionStoreData data)
    {
        var directory = Path.GetDirectoryName(_path);
        if (!string.IsNullOrEmpty(directory))
        {
            Directory.CreateDirectory(directory);
        }

        var json = JsonSerializer.Serialize(data, SerializerOptions);
        var tempPath = _path + ".tmp";
        File.WriteAllText(tempPath, json);
        File.Move(tempPath, _path, overwrite: true);
    }
}
