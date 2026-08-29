using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Soenneker.Cosmos.Container.Setup.Abstract;
using Soenneker.Cosmos.Database.Registrars;

namespace Soenneker.Cosmos.Container.Setup.Registrars;

/// <summary>
/// A utility library for Azure Cosmos container setup operations
/// </summary>
public static class CosmosContainerSetupUtilRegistrar
{
    /// <summary>
    /// Registers Cosmos Container Setup Util with a singleton lifetime.
    /// </summary>
    /// <param name="services">Service collection that receives the registration.</param>
    /// <returns>The same service collection, so additional registrations can be chained.</returns>
    public static IServiceCollection AddCosmosContainerSetupUtilAsSingleton(this IServiceCollection services)
    {
        services.AddCosmosDatabaseUtilAsSingleton().TryAddSingleton<ICosmosContainerSetupUtil, CosmosContainerSetupUtil>();

        return services;
    }

    /// <summary>
    /// Registers Cosmos Container Setup Util with a scoped lifetime.
    /// </summary>
    /// <param name="services">Service collection that receives the registration.</param>
    /// <returns>The same service collection, so additional registrations can be chained.</returns>
    public static IServiceCollection AddCosmosContainerSetupUtilAsScoped(this IServiceCollection services)
    {
        services.AddCosmosDatabaseUtilAsSingleton().TryAddScoped<ICosmosContainerSetupUtil, CosmosContainerSetupUtil>();

        return services;
    }
}
