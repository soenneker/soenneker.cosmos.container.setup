using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Fluent;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.Retry;
using Soenneker.Cosmos.Container.Setup.Abstract;
using Soenneker.Cosmos.Database.Abstract;
using Soenneker.Extensions.Task;
using Soenneker.Extensions.ValueTask;
using Soenneker.Utils.Random;

namespace Soenneker.Cosmos.Container.Setup;

/// <inheritdoc cref="ICosmosContainerSetupUtil"/>
public sealed class CosmosContainerSetupUtil : ICosmosContainerSetupUtil
{
    private readonly ILogger<CosmosContainerSetupUtil> _logger;
    private readonly ICosmosDatabaseUtil _cosmosDatabaseUtil;
    private readonly AsyncRetryPolicy _retryPolicy;

    public CosmosContainerSetupUtil(ILogger<CosmosContainerSetupUtil> logger, ICosmosDatabaseUtil cosmosDatabaseUtil)
    {
        _logger = logger;
        _cosmosDatabaseUtil = cosmosDatabaseUtil;
        _retryPolicy = Policy.Handle<CosmosException>(static exception =>
                                 exception.StatusCode is HttpStatusCode.RequestTimeout or HttpStatusCode.TooManyRequests or
                                     HttpStatusCode.InternalServerError or HttpStatusCode.ServiceUnavailable || (int)exception.StatusCode == 449)
                             .Or<HttpRequestException>()
                             .Or<TimeoutException>()
                             .WaitAndRetryAsync(5,
                                 static retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))
                                     + TimeSpan.FromMilliseconds(RandomUtil.Next(0, 1000)),
                                 (exception, timespan, retryCount, context) =>
                                 {
                                     _logger.LogWarning(exception,
                                         "*** CosmosContainerSetupUtil *** Failed to ensure container ({containerName}), trying again in {delay}s ... count: {retryCount}",
                                         context["containerName"], timespan.TotalSeconds, retryCount);
                                     return Task.CompletedTask;
                                 });
    }

    public async ValueTask<ContainerResponse?> Ensure(string containerName, CancellationToken cancellationToken = default)
    {
        Microsoft.Azure.Cosmos.Database database = await _cosmosDatabaseUtil.Get(cancellationToken)
                                                                            .NoSync();

        return await Ensure(database, containerName, cancellationToken)
            .NoSync();
    }

    public async ValueTask<ContainerResponse?> Ensure(string endpoint, string accountKey, string databaseName, string containerName,
        CancellationToken cancellationToken = default)
    {
        Microsoft.Azure.Cosmos.Database database = await _cosmosDatabaseUtil.Get(endpoint, accountKey, databaseName, cancellationToken)
                                                                            .NoSync();

        return await Ensure(database, containerName, cancellationToken)
            .NoSync();
    }

    public async ValueTask<ContainerResponse?> Ensure(Microsoft.Azure.Cosmos.Database database, string containerName,
        CancellationToken cancellationToken = default)
    {
        // These partition key paths need to match the serialized object property -exactly- (case sensitive)
        // We're going to keep these all as /partitionKey, and then identity what that value means within the C# document

        _logger.LogDebug("Ensuring Cosmos container ({containerName}) exists. If not, creating...", containerName);

        var containerBuilder = new ContainerBuilder(database, containerName, "/partitionKey");

        // TODO: Build indexing policy here

        ContainerResponse? containerResponse = null;

        var context = new Context {["containerName"] = containerName};
        await _retryPolicy.ExecuteAsync(async (_, token) =>
                         {
                             ThroughputProperties? containerThroughput = GetContainerThroughput(containerName);

                             containerResponse = await containerBuilder.CreateIfNotExistsAsync(containerThroughput, token)
                                                                       .NoSync();

                             _logger.LogDebug("Ensured container ({container})", containerName);
                         }, context, cancellationToken)
                         .NoSync();

        return containerResponse;
    }

    private ThroughputProperties? GetContainerThroughput(string containerName)
    {
        // TODO: Make container throughput here configurable

        ThroughputProperties? properties = null;

        _logger.LogDebug("Using no throughput settings for Cosmos container ({containerName})", containerName);

        return properties;
    }
}
