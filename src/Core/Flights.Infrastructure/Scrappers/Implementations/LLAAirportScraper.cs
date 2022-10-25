using Flights.Domain.ValueObjects;
using Flights.Infrastructure.Scrappers.Mappings.Dtos;
using Newtonsoft.Json;
using System.Collections.Concurrent;
using System.Net.Http.Json;
using System.Text.Json;

namespace Flights.Infrastructure.Scrappers.Implementations;

internal class LLAAirportScraper : ISearchableAirportScrapper
{
    private readonly IHttpClientFactory _httpClientFactory;

    public DestinationAirports ScrappedAirport => DestinationAirports.LLA;

    public LLAAirportScraper(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<IEnumerable<Flight>> ScrapeAsync()
    {
        using var httpClient = _httpClientFactory.CreateClient(HttpClientNames.LLA);

        var output = new ConcurrentStack<Flight>();
        
        await Parallel.ForEachAsync(Enumerable.Range(0, 10), async (value, ct) =>
        {
            Console.WriteLine(value);

            var messageResponse = await httpClient.PostAsJsonAsync("WebServices/FlightInformation.asmx/FlightArrivals", new { SearchTerm = value.ToString() });

            if (messageResponse.IsSuccessStatusCode)
            {
                var body = await messageResponse.Content.ReadAsStringAsync();
                var model = JsonConvert.DeserializeObject<LLAFlightInformation>(body);

                var mappedModels = Map(model.D.Where(m => m.IsArrival)).ToArray();

                if (mappedModels.Length > 0)
                {
                    output.PushRange(mappedModels);
                }
            }
        });


        var distinct = output.DistinctBy(m => m.FlightNumber).ToList();

        return distinct;
    }

    private IEnumerable<Flight> Map(IEnumerable<LLAAggregatedFlighInformation> models)
    {
        var output = new List<Flight>();

        foreach (var model in models)
        {
            var flightNumber = FlightDesignator.Create(model.Fltnmbr);

            if (flightNumber.IsSuccess)
            {
                output.Add(new Flight(
                    DateTime.Parse(model.Schedtime),
                    model.Origdest,
                    flightNumber.Value,
                    model.Message,
                    DestinationAirports.LLA));
            }
        }

        return output;
    }

    public async Task<IEnumerable<Flight>> SearchAsync(string search)
    {
        using var httpClient = _httpClientFactory.CreateClient(HttpClientNames.LLA);

        var messageResponse = await httpClient.PostAsJsonAsync("WebServices/FlightInformation.asmx/FlightArrivals", new { SearchTerm = search.ToUpper() });

        if (messageResponse.IsSuccessStatusCode == false)
        {
            return Array.Empty<Flight>();
        }

        var body = await messageResponse.Content.ReadAsStringAsync();
        var model = JsonConvert.DeserializeObject<LLAFlightInformation>(body);

        return Map(model.D.Where(m => m.IsArrival));
    }
}
