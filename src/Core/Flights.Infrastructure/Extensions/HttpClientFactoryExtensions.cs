using Flights.Infrastructure.Scrappers.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Net;

namespace Flights.Infrastructure.Extensions;

internal static class HttpClientFactoryExtensions
{
    public static IServiceCollection AddHttpClients(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<LHRScraperSettings>(configuration.GetSection(LHRScraperSettings.SettingsKey));
        services.Configure<LLAScraperSettings>(configuration.GetSection(LLAScraperSettings.SettingsKey));

        services.AddHttpClient(HttpClientNames.LLA, (sp, client) =>
        {
            var settings = sp.GetRequiredService<IOptions<LLAScraperSettings>>().Value;

            client.BaseAddress = new Uri(settings.BaseUrl);
        });

        services.AddHttpClient(HttpClientNames.LHR, (sp, client) =>
        {
            var settings = sp.GetRequiredService<IOptions<LHRScraperSettings>>().Value;

            client.DefaultRequestHeaders.Add("Origin", "https://www.heathrow.com");
            client.DefaultRequestHeaders.Add("User-Agent", "Flights");

            client.BaseAddress = new Uri(settings.BaseUrl);
        })
        .ConfigurePrimaryHttpMessageHandler(() =>
        {
            return new HttpClientHandler() { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate };
        });

        return services;
    }
}
