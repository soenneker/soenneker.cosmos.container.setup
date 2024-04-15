using Microsoft.Azure.Cosmos;
using System.Threading.Tasks;

namespace Soenneker.Cosmos.Container.Setup.Abstract;

/// <summary>
/// Singleton
/// </summary>
public interface ICosmosContainerSetupUtil
{
    ValueTask<ContainerResponse?> Ensure(string name);

    ValueTask<ContainerResponse?> Ensure(string name, string databaseName);

    ValueTask<ContainerResponse?> Ensure(Microsoft.Azure.Cosmos.Database database, string containerName);
}