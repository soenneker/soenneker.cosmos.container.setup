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
    public static IServiceCollection AddCosmosContainerSetupUtilAsSingleton(this IServiceCollection services)
    {
        services.AddCosmosDatabaseUtilAsSingleton().TryAddSingleton<ICosmosContainerSetupUtil, CosmosContainerSetupUtil>();

        return services;
    }

    public static IServiceCollection AddCosmosContainerSetupUtilAsScoped(this IServiceCollection services)
    {
        services.AddCosmosDatabaseUtilAsSingleton().TryAddScoped<ICosmosContainerSetupUtil, CosmosContainerSetupUtil>();

        return services;
    }
}