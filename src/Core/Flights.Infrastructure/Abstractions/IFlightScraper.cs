namespace Flights.Infrastructure.Abstractions;

public interface IFlightScraper
{
    Task<IEnumerable<Flight>> GetAllAsync(DestinationAirports airport);

    Task<Result<IEnumerable<Flight>>> SearchAsync(DestinationAirports airport, string search);
}
