using Flights.Application.Core.Behaviours;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Flights.Application;

public static class DependencyInjections
{
    public static IServiceCollection AddFlightsApplication(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(typeof(AssemblyReference).Assembly);

        services.AddMediatR(typeof(AssemblyReference).Assembly);

        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

        return services;
    }
}
