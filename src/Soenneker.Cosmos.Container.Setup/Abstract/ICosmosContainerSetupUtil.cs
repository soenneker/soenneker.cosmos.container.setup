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
    /// Executes the ensure operation.
    /// </summary>
    /// <param name="containerName">The container name.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task containing the result of the operation.</returns>
    ValueTask<ContainerResponse?> Ensure(string containerName, CancellationToken cancellationToken = default);

    /// <summary>
    /// Executes the ensure operation.
    /// </summary>
    /// <param name="endpoint">The endpoint.</param>
    /// <param name="accountKey">The account key.</param>
    /// <param name="databaseName">The database name.</param>
    /// <param name="containerName">The container name.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task containing the result of the operation.</returns>
    ValueTask<ContainerResponse?> Ensure(string endpoint, string accountKey, string databaseName, string containerName, CancellationToken cancellationToken = default);

    /// <summary>
    /// Executes the ensure operation.
    /// </summary>
    /// <param name="database">The database.</param>
    /// <param name="containerName">The container name.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task containing the result of the operation.</returns>
    ValueTask<ContainerResponse?> Ensure(Microsoft.Azure.Cosmos.Database database, string containerName, CancellationToken cancellationToken = default);
}