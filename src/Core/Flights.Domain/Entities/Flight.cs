namespace Flights.Domain.Entities;

public class Flight : Entity
{
    public DateTime ScheduledAt { get; set; }

    public DestinationAirports Destination { get; set; }

    public string From { get; set; }

    public FlightDesignator FlightNumber { get; set; }

    public string Status { get; set; }

    public Flight(
        DateTime scheduledAt,
        string from,
        FlightDesignator flightNumber,
        string status,
        DestinationAirports destination)
        : this()
    {
        ScheduledAt = scheduledAt;
        From = from;
        FlightNumber = flightNumber;
        Status = status;
        Destination = destination;
    }

    public Flight()
        : base(Guid.NewGuid())
    {

    }
}
