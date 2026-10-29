namespace StripeWebAPI.Services;

/// <summary>
/// Small helpers for building the absolute URLs Stripe requires for redirects.
/// </summary>
public static class AppUrls
{
    /// <summary>
    /// Origin to use for redirects: the configured Stripe:AppOrigin when set, otherwise the
    /// origin of the incoming request (never derive it from a Host header when a config value exists).
    /// </summary>
    public static string Origin(HttpRequest request, string? configuredOrigin)
    {
        var configured = configuredOrigin?.Trim() ?? "";
        if (configured.StartsWith("http://", StringComparison.OrdinalIgnoreCase) ||
            configured.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
        {
            return configured.TrimEnd('/');
        }

        return $"{request.Scheme}://{request.Host.Value}";
    }

    /// <summary>
    /// Makes <paramref name="value"/> absolute against <paramref name="origin"/>. Accepts an
    /// absolute URL, a root-relative path (/done) or nothing at all (falls back to <paramref name="fallbackPath"/>).
    /// </summary>
    public static string Absolute(string origin, string? value, string fallbackPath)
    {
        var result = string.IsNullOrWhiteSpace(value) ? fallbackPath : value.Trim();

        if (result.StartsWith("http://", StringComparison.OrdinalIgnoreCase) ||
            result.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
        {
            return result;
        }

        if (!result.StartsWith("/", StringComparison.Ordinal))
        {
            result = "/" + result;
        }

        return origin.TrimEnd('/') + result;
    }
}
