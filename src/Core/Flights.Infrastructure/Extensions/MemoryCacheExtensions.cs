using Microsoft.Extensions.Caching.Memory;

namespace Flights.Infrastructure.Extensions;

internal static class MemoryCacheExtensions
{
    internal static string GenerateCacheKey(this IMemoryCache _, DestinationAirports airport)
    {
        const string cacheKeyPrefix = "FLIGHTS";

        return $"{cacheKeyPrefix}_{airport}";
    }
}
