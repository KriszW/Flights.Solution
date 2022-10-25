using Flights.Domain.Entities;

namespace Flights.Web.Store.Flights.Actions;

public class FlightsLoadadAction
{
    public IEnumerable<Flight> NewFlights { get; set; }

    public FlightsLoadadAction(IEnumerable<Flight> newFlights)
    {
        NewFlights = newFlights;
    }
}
