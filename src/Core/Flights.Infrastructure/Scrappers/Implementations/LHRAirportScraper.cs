
using Flights.Domain.ValueObjects;
using Flights.Infrastructure.Scrappers.Mappings.Dtos;
using Newtonsoft.Json;
using System.Data;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using static Flights.Infrastructure.Scrappers.Mappings.Dtos.LHRAircraftMovement.LHRRoute;

namespace Flights.Infrastructure.Scrappers.Implementations;

internal class LHRAirportScraper : ISearchableAirportScrapper
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IDateTime _dateTime;

    public DestinationAirports ScrappedAirport => DestinationAirports.LHR;

    public LHRAirportScraper(IHttpClientFactory httpClientFactory, IDateTime dateTime)
    {
        _httpClientFactory = httpClientFactory;
        _dateTime = dateTime;
    }

    public async Task<IEnumerable<Flight>> ScrapeAsync()
    {
        using var httpClient = _httpClientFactory.CreateClient(HttpClientNames.LHR);

        var requestMessage = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri($"{httpClient.BaseAddress}pihub/flights/arrivals?date={_dateTime.UtcNow:yyyy-MM-dd}&orderBy=localArrivalTime&excludeCodeShares=true")
        };

        var messageResponse = await httpClient.SendAsync(requestMessage);

        if (messageResponse.IsSuccessStatusCode == false)
        {
            return Array.Empty<Flight>();
        }

        var body = await messageResponse.Content.ReadAsStringAsync();

        var models = JsonConvert.DeserializeObject<IEnumerable<LHRFlightInformation>>(body);

        return Map(models?.Where(m => m.FlightService.ArrivalOrDeparture == LHRFlightInformation.ArrivingFlag));
    }

    private IEnumerable<Flight> Map(IEnumerable<LHRFlightInformation> models)
    {
        var output = new List<Flight>();

        foreach (var model in models)
        {
            var flightNumber = FlightDesignator.Create(model.FlightService.IataFlightIdentifier);

            var originPort = model.FlightService.AircraftMovement.Route.PortsOfCall.FirstOrDefault(poc => poc.PortOfCallType == LHRPortsOfCall.OriginFlag);

            var destinationPort = model.FlightService.AircraftMovement.Route.PortsOfCall.FirstOrDefault(poc => poc.PortOfCallType == LHRPortsOfCall.DestinationFlag);

            if (flightNumber.IsSuccess)
            {
                output.Add(new Flight(
                    destinationPort.OperatingTimes.Scheduled.Local,
                    originPort.AirportFacility.AirportCityLocation.Name,
                    flightNumber.Value,
                    model.FlightService.AircraftMovement.AircraftMovementStatus.First().Message,
                    DestinationAirports.LHR));
            }
        }

        return output;
    }

    public async Task<IEnumerable<Flight>> SearchAsync(string search)
    {
        using var httpClient = _httpClientFactory.CreateClient(HttpClientNames.LHR);

        var uri = $"{httpClient.BaseAddress}enterprisesearch/prod/odata/flights?Search={search}*&$filter=(destinationScheduledDateTime ge {_dateTime.UtcNow.Date:yyyy-MM-ddT00:00:00Z} and destinationScheduledDateTime le {_dateTime.UtcNow.AddDays(1).Date:yyyy-MM-ddT00:00:00Z}) and direction eq 'A'&$orderby=destinationScheduledDateTime asc";

        var requestMessage = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri(uri)
        };

        var messageResponse = await httpClient.SendAsync(requestMessage);

        var body = await messageResponse.Content.ReadAsStringAsync();

        if (messageResponse.IsSuccessStatusCode == false)
        {
            return Array.Empty<Flight>();
        }

        var models = JsonConvert.DeserializeObject<LHRSearch>(body);

        return Map(models?.Value?.Where(m => m.Direction == LHRFlightInformation.ArrivingFlag));
    }

    private IEnumerable<Flight> Map(IEnumerable<LHRSearch.LHRSearchInfo> models)
    {
        var output = new List<Flight>();

        foreach (var model in models)
        {
            var flightNumber = FlightDesignator.Create(model.FlightNumber);

            if (flightNumber.IsSuccess)
            {
                output.Add(new Flight(
                    model.DestinationScheduledDateTime,
                    model.OriginAirportCity,
                    flightNumber.Value,
                    model.Status.Last().Message,
                    DestinationAirports.LHR));
            }
        }

        return output;
    }
}
