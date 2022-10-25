namespace Flights.Domain.Core.Errors;

public static class DomainErrors
{
    public static class General
    {
        public static Error UnProcessableRequest => new("General.UnProcessableRequest", "The server could not process the request.");

        public static Error ServerError => new("General.ServerError", "The server encountered an unrecoverable error.");

        public static Error SearchNotSupported => new("General.SearchNotSupported", "Search is not supported for this airport");
    }

    public static class FlightDesignator
    {
        public static Error NullOrEmpty => new("FlightDesignator.NullOrEmpty", "The airport code can not be null");

        public static Error Invalid => new("FlightDesignator.Invalid", "The flightnumber is invalid");
    }
}
