namespace Flights.Application.Flights.Queries;

public static class LoadPaginatedFlightsQuery
{
    public record Request(DestinationAirports Airport, int Page, int PageSize) : IQuery<Response>;

    internal class Handler : IQueryHandler<Request, Response>
    {
        private readonly IFlightsRepository _flightsRepository;

        public Handler(IFlightsRepository flightsRepository)
        {
            _flightsRepository = flightsRepository;
        }

        public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
        {
            var flights = await _flightsRepository.GetAllAsync(request.Airport);

            var totalCount = flights.Count();
            return new(new PagedList<Flight>(flights.Skip(request.Page * request.PageSize).Take(request.PageSize), request.Page, request.PageSize, totalCount));
        }
    }

    public record Response(Result<PagedList<Flight>> Flights);

    public class Validator : AbstractValidator<Request>
    {
        public Validator()
        {
            RuleFor(m => m.Page)
                .GreaterThanOrEqualTo(0).WithError(ValidationErrors.Pagination.NegativePage);

            RuleFor(m => m.PageSize)
                .GreaterThanOrEqualTo(0).WithError(ValidationErrors.Pagination.NegativePageSize);
        }
    }
}
