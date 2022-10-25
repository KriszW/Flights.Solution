using Flights.Domain.Core.Errors;
using Flights.Infrastructure.Extensions;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;

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
        var json = _memoryCache.Get<string>(_memoryCache.GenerateCacheKey(airport));

        if (json is null) return await _flightScraper.GetAllAsync(airport);

        return JsonConvert.DeserializeObject<IEnumerable<Flight>>(json);
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
}
