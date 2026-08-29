using Microsoft.Azure.Cosmos;
using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Cosmos.Container.Setup.Abstract;

/// <summary>
/// Singleton
/// </summary>
public interface ICosmosContainerSetupUtil
{
    /// <summary>
    /// Ensures cosmos container setup for the cosmos container setup.
    /// </summary>
    /// <param name="containerName">Name of the container to target.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task whose result is the requested container Response.</returns>
    ValueTask<ContainerResponse?> Ensure(string containerName, CancellationToken cancellationToken = default);

    /// <summary>
    /// Ensures cosmos container setup for the cosmos container setup.
    /// </summary>
    /// <param name="endpoint">Service endpoint to call.</param>
    /// <param name="accountKey">Account key used for authentication.</param>
    /// <param name="databaseName">Name of the target database.</param>
    /// <param name="containerName">Name of the container to target.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task whose result is the requested container Response.</returns>
    ValueTask<ContainerResponse?> Ensure(string endpoint, string accountKey, string databaseName, string containerName, CancellationToken cancellationToken = default);

    /// <summary>
    /// Ensures cosmos Container Setup.
    /// </summary>
    /// <param name="database">Database for the ensure operation.</param>
    /// <param name="containerName">Name of the container to target.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task whose result is the requested container Response.</returns>
    ValueTask<ContainerResponse?> Ensure(Microsoft.Azure.Cosmos.Database database, string containerName, CancellationToken cancellationToken = default);
}
