using Flights.Domain.Core.Errors;
using Microsoft.Extensions.DependencyInjection;

namespace Flights.Infrastructure.Scrappers;

internal class InMemoryFlightScraper : IFlightScraper
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public InMemoryFlightScraper(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    public async Task<IEnumerable<Flight>> GetAllAsync(DestinationAirports airport)
    {
        await using var scope = _serviceScopeFactory.CreateAsyncScope();

        var airportScrapers = scope.ServiceProvider.GetRequiredService<IEnumerable<IAirportScraper>>();

        foreach (var scraper in airportScrapers)
        {
            if (scraper.ScrappedAirport == airport)
            {
                return await scraper.ScrapeAsync();
            }
        }

        throw new InvalidOperationException($"There is not scraper for {airport}");
    }

    public async Task<Result<IEnumerable<Flight>>> SearchAsync(DestinationAirports airport, string search)
    {
        await using var scope = _serviceScopeFactory.CreateAsyncScope();

        var searchableAirportScrapers = scope.ServiceProvider.GetRequiredService<IEnumerable<ISearchableAirportScrapper>>();

        foreach (var scraper in searchableAirportScrapers)
        {
            if (scraper.ScrappedAirport == airport)
            {
                return Result.Success(await scraper.SearchAsync(search));
            }
        }

        return Result.Failure<IEnumerable<Flight>>(DomainErrors.General.SearchNotSupported);
    }
}
