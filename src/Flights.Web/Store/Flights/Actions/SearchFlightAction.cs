using Flights.Domain.Enumerations;

namespace Flights.Web.Store.Flights.Actions;

public class SearchFlightAction
{
    public SearchFlightAction(string search, DestinationAirports airport)
    {
        Search = search;
        Airport = airport;
    }

    public string Search { get; set; }

    public DestinationAirports Airport { get; set; }
}
