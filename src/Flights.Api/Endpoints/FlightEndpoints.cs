using Microsoft.AspNetCore.Mvc;

namespace Flights.Api.Endpoints;

internal static class FlightEndpoints
{
    internal static IEndpointRouteBuilder MapFlightEndpoints(this IEndpointRouteBuilder builder)
    {
        builder.MapGet(ApiRoutes.Flights.Get, async ([FromServices] IMediator mediator, [FromRoute] DestinationAirports airport, [FromQuery] int page, [FromQuery] int pageSize) =>
        {
            var result = await mediator.Send(new LoadPaginatedFlightsQuery.Request(airport, page, pageSize));

            return result.Flights.IsFailure
                ? Results.BadRequest(new ApiErrorResponse(new[] { result.Flights.Error }))
                : Results.Ok(result.Flights.Value);
        })
        .Produces<PagedList<IEnumerable<Flight>>>(200)
        .Produces<ApiErrorResponse>(400)
        .WithTags("flights")
        .WithDisplayName("Get flights for airport");

        builder.MapGet(ApiRoutes.Flights.Search, async ([FromServices] IMediator mediator, [FromRoute] DestinationAirports airport, [FromQuery] string search, [FromQuery] int page, [FromQuery] int pageSize) =>
        {
            if (string.IsNullOrEmpty(search))
            {
                var result = await mediator.Send(new LoadPaginatedFlightsQuery.Request(airport, page, pageSize));

                return result.Flights.IsFailure
                    ? Results.BadRequest(new ApiErrorResponse(new[] { result.Flights.Error }))
                    : Results.Ok(result.Flights.Value);
            }
            else
            {
                var result = await mediator.Send(new SearchPaginatedFlightsQuery.Request(airport, search, page, pageSize));

                return result.Flights.IsFailure
                    ? Results.BadRequest(new ApiErrorResponse(new[] { result.Flights.Error }))
                    : Results.Ok(result.Flights.Value);
            }
        })
        .Produces<PagedList<IEnumerable<Flight>>>(200)
        .Produces<ApiErrorResponse>(400)
        .WithTags("flights")
        .WithDisplayName("Search flights in airport");

        return builder;
    }
}
