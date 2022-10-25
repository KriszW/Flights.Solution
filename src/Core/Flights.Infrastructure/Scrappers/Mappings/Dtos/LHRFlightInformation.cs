namespace Flights.Infrastructure.Scrappers.Mappings.Dtos;

internal class LHRFlightInformation
{
    public const string ArrivingFlag = "A";


    public LHRFlightService FlightService { get; set; }
}

internal class LHRFlightService
{
    public string IataFlightIdentifier { get; set; }

    public string ArrivalOrDeparture { get; set; }

    public LHRAircraftMovement AircraftMovement { get; set; }
}

internal class LHRAircraftMovement
{

    public IEnumerable<LHRAircraftMovementStatus> AircraftMovementStatus { get; set; }

    public LHRRoute Route { get; set; }

    internal class LHRAircraftMovementStatus
    {
        public string Name { get; set; }

        public string Message { get; set; }

        public IEnumerable<LocalizedStatusData> StatusData { get; set; }

        internal class LocalizedStatusData
        {
            public string LocalisationKey { get; set; }
        }
    }

    internal class LHRRoute
    {
        public IEnumerable<LHRPortsOfCall> PortsOfCall { get; set; }

        internal class LHRPortsOfCall
        {
            public const string OriginFlag = "ORIGIN";
            public const string DestinationFlag = "DESTINATION";

            public string PortOfCallType { get; set; }

            public LHRAirportFacility AirportFacility { get; set; }

            public LHROperatingTimes OperatingTimes { get; set; }

            internal class LHROperatingTimes
            {
                public LHRScheduled Scheduled { get; set; }

                internal class LHRScheduled
                {
                    public DateTime Utc { get; set; }

                    public DateTime Local { get; set; }
                }
            }

            internal class LHRAirportFacility
            {
                public LHRAirportCityLocation AirportCityLocation { get; set; }

                internal class LHRAirportCityLocation
                {
                    public string Name { get; set; }
                }
            }
        }
    }
}