namespace Flights.Infrastructure.Abstractions;

internal interface ISearchableAirportScrapper : IAirportScraper
{
    Task<IEnumerable<Flight>> SearchAsync(string search);
}
