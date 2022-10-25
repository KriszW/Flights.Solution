using Flights.Web.Store.Flights.Actions;

namespace Flights.Web.Store.Flights;

public static class FlightReducers
{
    [ReducerMethod]
    public static FlightsState Loaded(FlightsState state, FlightsLoadadAction action) => state with { Flights = action.NewFlights };

    [ReducerMethod]
    public static FlightsState LoadingRequested(FlightsState state, LoadFlightAction action) => state with { Flights = default };

    [ReducerMethod]
    public static FlightsState SearchRequested(FlightsState state, SearchFlightAction action) => state with { Flights = default };
}
