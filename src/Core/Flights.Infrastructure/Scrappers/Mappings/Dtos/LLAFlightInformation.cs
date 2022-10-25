namespace Flights.Infrastructure.Scrappers.Mappings.Dtos;

internal class LLAFlightInformation
{
    public IEnumerable<LLAAggregatedFlighInformation> D { get; set; }
}

internal class LLAAggregatedFlighInformation
{
    public string Message { get; set; }

    public string Fltnmbr { get; set; }

    public string Origdest { get; set; }

    public string Schedtime { get; set; }

    public bool IsArrival { get; set; }
}
