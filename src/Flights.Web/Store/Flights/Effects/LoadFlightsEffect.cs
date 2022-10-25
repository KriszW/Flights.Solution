using Flights.Contracts.Common;
using Flights.Domain.Core.Primitives.Result;
using Flights.Domain.Entities;
using Flights.Web.Store.Flights.Actions;
using System.Net.Http.Json;
using System.Text.Json;

namespace Flights.Web.Store.Flights.Effects;

public class LoadFlightsEffect : Effect<LoadFlightAction>
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<LoadFlightsEffect> _logger;

    public LoadFlightsEffect(HttpClient httpClient, ILogger<LoadFlightsEffect> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    public override async Task HandleAsync(LoadFlightAction action, IDispatcher dispatcher)
    {
        var index = (int)action.Airport;

        try
        {
            var data = await _httpClient.GetFromJsonAsync<PagedList<Flight>>($"api/flights/{index}?page=0&pageSize=10");

            dispatcher.Dispatch(new FlightsLoadadAction(data.Items ?? Array.Empty<Flight>()));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "There was an error");
        }
    }
}
