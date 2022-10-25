using Flights.Domain.Entities;

namespace Flights.Web.Store.Flights;

[FeatureState]
public record FlightsState
{
    public IEnumerable<Flight> Flights { get; set; }
}
