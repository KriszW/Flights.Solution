namespace Flights.Infrastructure.Abstractions;

public interface IAirportScraper
{
    DestinationAirports ScrappedAirport { get; }

    Task<IEnumerable<Flight>> ScrapeAsync();
}
