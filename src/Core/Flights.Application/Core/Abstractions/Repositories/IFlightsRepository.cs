namespace Flights.Application.Core.Abstractions.Repositories;

public interface IFlightsRepository
{
    Task<IEnumerable<Flight>> GetAllAsync(DestinationAirports airport);

    Task<IEnumerable<Flight>> SearchAsync(DestinationAirports airport, string search);
}
