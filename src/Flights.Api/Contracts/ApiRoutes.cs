namespace Flights.Api.Contracts;

internal static class ApiRoutes
{
    internal static class Flights
    {
        public const string Get = "api/flights/{airport}";

        public const string Search = "api/flights/{airport}/search";
    }
}
