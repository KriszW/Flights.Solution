namespace Flights.Infrastructure.Scrappers.Mappings.Dtos;

internal class LHRSearch
{
    public LHRSearchInfo[] Value { get; set; }

    internal class LHRSearchInfo
    {
        public string OriginAirportCity { get; set; }

        public DateTime DestinationScheduledDateTime { get; set; }

        public string FlightNumber { get; set; }

        public LHRAircraftMovementStatus[] Status { get; set; }

        public string Direction { get; set; }

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
    }
}
