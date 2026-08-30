using Microsoft.Azure.Cosmos;
using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Cosmos.Container.Setup.Abstract;

/// <summary>
/// Ensures Azure Cosmos DB containers exist with the package's standard container settings.
/// </summary>
public interface ICosmosContainerSetupUtil
{
    /// <summary>
    /// Ensures a container exists in the configured default database.
    /// </summary>
    /// <param name="containerName">Name of the container to target.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task whose result is the create-or-existing response.</returns>
    ValueTask<ContainerResponse?> Ensure(string containerName, CancellationToken cancellationToken = default);

    /// <summary>
    /// Ensures a container exists in an explicit account and database.
    /// </summary>
    /// <param name="endpoint">Service endpoint to call.</param>
    /// <param name="accountKey">Account key used for authentication.</param>
    /// <param name="databaseName">Name of the target database.</param>
    /// <param name="containerName">Name of the container to target.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task whose result is the create-or-existing response.</returns>
    ValueTask<ContainerResponse?> Ensure(string endpoint, string accountKey, string databaseName, string containerName, CancellationToken cancellationToken = default);

    /// <summary>
    /// Ensures a container exists in the supplied database.
    /// </summary>
    /// <param name="database">Database for the ensure operation.</param>
    /// <param name="containerName">Name of the container to target.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task whose result is the create-or-existing response.</returns>
    ValueTask<ContainerResponse?> Ensure(Microsoft.Azure.Cosmos.Database database, string containerName, CancellationToken cancellationToken = default);
}
