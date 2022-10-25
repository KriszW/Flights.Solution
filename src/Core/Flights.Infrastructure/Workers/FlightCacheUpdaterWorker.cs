using Flights.Infrastructure.Extensions;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Flights.Infrastructure.Workers;

internal class FlightCacheUpdaterWorker : BackgroundService
{
    private readonly IFlightScraper _flightScraper;
    private readonly IMemoryCache _memoryCache;
    private readonly ILogger<FlightCacheUpdaterWorker> _logger;

    public FlightCacheUpdaterWorker(
        IFlightScraper flightScraper,
        IMemoryCache memoryCache,
        ILogger<FlightCacheUpdaterWorker> logger)
    {
        _flightScraper = flightScraper;
        _memoryCache = memoryCache;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (stoppingToken.IsCancellationRequested == false)
        {
            foreach (var airport in Enum.GetValues<DestinationAirports>())
            {
                try
                {
                    var cacheKey = _memoryCache.GenerateCacheKey(airport);

                    var flights = await _flightScraper.GetAllAsync(airport);

                    _memoryCache.Set(cacheKey, JsonConvert.SerializeObject(flights), new MemoryCacheEntryOptions
                    {
                        AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(10)
                    });
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"There was en error while updating flight informations for {airport}");
                }
            }

            await Task.Delay(TimeSpan.FromSeconds(1), stoppingToken);
        }
    }
}
