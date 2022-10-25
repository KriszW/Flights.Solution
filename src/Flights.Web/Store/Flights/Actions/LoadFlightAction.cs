using Flights.Domain.Enumerations;

namespace Flights.Web.Store.Flights.Actions;

public class LoadFlightAction
{
    public DestinationAirports Airport { get; set; }

    public LoadFlightAction(DestinationAirports airport)
    {
        Airport = airport;
    }
}
