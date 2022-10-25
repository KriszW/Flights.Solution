using Flights.Contracts.Common;
using Flights.Domain.Entities;
using Flights.Web.Store.Flights.Actions;
using System.Net.Http.Json;

namespace Flights.Web.Store.Flights.Effects;

public class SearchFlightsEffect : Effect<SearchFlightAction>
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<LoadFlightsEffect> _logger;

    public SearchFlightsEffect(HttpClient httpClient, ILogger<LoadFlightsEffect> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    public override async Task HandleAsync(SearchFlightAction action, IDispatcher dispatcher)
    {
        var index = (int)action.Airport;

        try
        {
            var data = await _httpClient.GetFromJsonAsync<PagedList<Flight>>($"api/flights/{index}/search?search={action.Search}&page=0&pageSize=10");

            dispatcher.Dispatch(new FlightsLoadadAction(data.Items ?? Array.Empty<Flight>()));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "There was an error");
        }
    }
}
