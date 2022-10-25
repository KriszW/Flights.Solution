using Flights.Infrastructure.Scrappers;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Flights.Infrastructure.Extensions;

internal static class ServiceCollectionExtensions
{
    public static IServiceCollection AddScrapers(this IServiceCollection services, params Assembly[] assemblies)
    {
        services.AddBasicScrapers(assemblies);

        services.AddSearchableScrapers(assemblies);

        services.AddSingleton<IFlightScraper, InMemoryFlightScraper>();

        return services;
    }

    private static IServiceCollection AddBasicScrapers(this IServiceCollection services, Assembly[] assemblies)
    {
        foreach (var assembly in assemblies)
        {
            var scrapers = assembly.GetTypes().Where(x => !x.IsAbstract && x.IsClass && x.GetInterface(nameof(IAirportScraper)) == typeof(IAirportScraper));

            foreach (var scraper in scrapers)
            {
                services.Add(new ServiceDescriptor(typeof(IAirportScraper), scraper, ServiceLifetime.Scoped));
            }
        }

        return services;
    }

    private static IServiceCollection AddSearchableScrapers(this IServiceCollection services, Assembly[] assemblies)
    {
        foreach (var assembly in assemblies)
        {
            var scrapers = assembly.GetTypes().Where(x => !x.IsAbstract && x.IsClass && x.GetInterface(nameof(ISearchableAirportScrapper)) == typeof(ISearchableAirportScrapper));

            foreach (var scraper in scrapers)
            {
                services.Add(new ServiceDescriptor(typeof(ISearchableAirportScrapper), scraper, ServiceLifetime.Scoped));
            }
        }

        return services;
    }
}
