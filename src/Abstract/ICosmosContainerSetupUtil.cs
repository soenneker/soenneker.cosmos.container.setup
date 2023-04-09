using Microsoft.Azure.Cosmos;
using System.Threading.Tasks;

namespace Soenneker.Cosmos.Container.Setup.Abstract;

/// <summary>
/// Singleton
/// </summary>
public interface ICosmosContainerSetupUtil
{
    ValueTask<ContainerResponse?> EnsureContainer(string name);

    ValueTask<ContainerResponse?> EnsureContainer(string name, string databaseName);

    ValueTask<ContainerResponse?> EnsureContainer(Microsoft.Azure.Cosmos.Database database, string containerName);
}