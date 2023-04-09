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
    /// As Singleton
    /// </summary>
    public static void AddCosmosContainerSetupUtil(this IServiceCollection services)
    {
        services.AddCosmosDatabaseUtil();
        services.TryAddSingleton<ICosmosContainerSetupUtil, CosmosContainerSetupUtil>();
    }
}