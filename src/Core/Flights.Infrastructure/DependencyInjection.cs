using Flights.Infrastructure.Common;
using Flights.Infrastructure.Extensions;
using Flights.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Flights.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddFlightsInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMemoryCache();

            services.AddScrapers(typeof(AssemblyReference).Assembly);
            
            services.AddHttpClients(configuration);

            services.AddTransient<IDateTime, MachineDateTime>();

            services.AddScoped<IFlightsRepository, InMemoryCacheFlightsRepository>();

            return services;
        }
    }
}
