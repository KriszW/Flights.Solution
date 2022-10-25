namespace Flights.Application.Core.Errors;

internal static class ValidationErrors
{
    public static class Pagination
    {
        public static Error NegativePage => new("Pagination.NegativePage", "Page number cannot be negative.");

        public static Error NegativePageSize => new("Pagination.NegativePageSize", "Page size cannot be negative.");
    }

    public static class Flights
    {
        public static class Search
        {
            public static Error Empty => new("Flights.Search.Empty", "Search query cannot be empty.");
        }
    }
}