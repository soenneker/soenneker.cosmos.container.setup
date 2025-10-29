using Microsoft.Azure.Cosmos;
using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Cosmos.Container.Setup.Abstract;

/// <summary>
/// Singleton
/// </summary>
public interface ICosmosContainerSetupUtil
{
    ValueTask<ContainerResponse?> Ensure(string containerName, CancellationToken cancellationToken = default);

    ValueTask<ContainerResponse?> Ensure(string databaseName, string containerName, CancellationToken cancellationToken = default);

    ValueTask<ContainerResponse?> Ensure(Microsoft.Azure.Cosmos.Database database, string containerName, CancellationToken cancellationToken = default);
}