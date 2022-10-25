using System.Text.RegularExpressions;

namespace Flights.Domain.ValueObjects;

public class FlightDesignator : ValueObject
{
    // SOURCE: https://github.com/jhermsmeier/node-flight-designator/blob/master/lib/flight-designator.js
    private static Regex CommonFlightDesignatorValidator => new("^([A-Z0-9]{2}[A-Z]?)\\s*([0-9]{1,4})\\s*([A-Z]?)$", RegexOptions.Compiled);

    public string Value { get; set; }

    private FlightDesignator(string value)
        : this()
    {
        Value = value;
    }

    public FlightDesignator()
    {

    }

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }

    public static Result<FlightDesignator> Create(string designator) =>
        Result.Create(designator, DomainErrors.FlightDesignator.NullOrEmpty)
            .Ensure(d => !string.IsNullOrWhiteSpace(d), DomainErrors.FlightDesignator.NullOrEmpty)
            .Ensure(d => CommonFlightDesignatorValidator.IsMatch(d), DomainErrors.FlightDesignator.Invalid)
            .Map(d => new FlightDesignator(d));

    public override string ToString()
    {
        return Value;
    }
}
