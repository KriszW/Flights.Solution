using Flights.Domain.Core.Errors;
using Microsoft.Extensions.Caching.Memory;

namespace Flights.Infrastructure.Repositories;

internal class InMemoryCacheFlightsRepository : IFlightsRepository
{
    private readonly IFlightScraper _flightScraper;
    private readonly IMemoryCache _memoryCache;

    public InMemoryCacheFlightsRepository(
        IFlightScraper flightScraper,
        IMemoryCache memoryCache)
    {
        _flightScraper = flightScraper;
        _memoryCache = memoryCache;
    }

    public async Task<IEnumerable<Flight>> GetAllAsync(DestinationAirports airport)
    {
        return await _memoryCache.GetOrCreateAsync(GenerateCacheKey(airport), cacheEntry =>
        {
            SetCacheSettings.Invoke(cacheEntry);
            return _flightScraper.GetAllAsync(airport);
        });
    }

    public async Task<IEnumerable<Flight>> SearchAsync(DestinationAirports airport, string search)
    {
        var searchResult = await _flightScraper.SearchAsync(airport, search);

        if (searchResult.IsFailure && searchResult.Error.Code == DomainErrors.General.SearchNotSupported)
        {
            var data = await GetAllAsync(airport);

            return data.Where(m => m.FlightNumber.Value.Contains(search));
        }

        return searchResult.Value;
    }

    private static Action<ICacheEntry> SetCacheSettings => entry =>
    {
        entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(3);
    };

    private static string GenerateCacheKey(DestinationAirports airport)
    {
        const string cacheKeyPrefix = "FLIGHTS";

        return $"{cacheKeyPrefix}_{airport}";
    }
}
