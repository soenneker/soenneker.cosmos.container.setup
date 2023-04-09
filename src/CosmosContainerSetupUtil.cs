using System;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Fluent;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.Retry;
using Soenneker.Cosmos.Container.Setup.Abstract;
using Soenneker.Cosmos.Database.Abstract;
using Soenneker.Utils.Random;

namespace Soenneker.Cosmos.Container.Setup;

///<inheritdoc cref="ICosmosContainerSetupUtil"/>
public class CosmosContainerSetupUtil : ICosmosContainerSetupUtil
{
    private readonly ILogger<CosmosContainerSetupUtil> _logger;
    private readonly ICosmosDatabaseUtil _cosmosDatabaseUtil;

    public CosmosContainerSetupUtil(ILogger<CosmosContainerSetupUtil> logger, ICosmosDatabaseUtil cosmosDatabaseUtil)
    {
        _logger = logger;
        _cosmosDatabaseUtil = cosmosDatabaseUtil;
    }

    public async ValueTask<ContainerResponse?> EnsureContainer(string name)
    {
        Microsoft.Azure.Cosmos.Database database = await _cosmosDatabaseUtil.GetDatabase();

        ContainerResponse? result = await EnsureContainer(database, name);

        return result;
    }

    public async ValueTask<ContainerResponse?> EnsureContainer(string name, string databaseName)
    {
        Microsoft.Azure.Cosmos.Database database = await _cosmosDatabaseUtil.GetDatabase(databaseName);

        ContainerResponse? result = await EnsureContainer(database, name);

        return result;
    }

    public async ValueTask<ContainerResponse?> EnsureContainer(Microsoft.Azure.Cosmos.Database database, string containerName)
    {
        // These partition key paths need to match the serialized object property -exactly- (case sensitive)
        // We're going to keep these all as /partitionKey, and then identity what that value means within the C# document

        _logger.LogDebug("Ensuring Cosmos container ({container}) exists. If not, creating...", containerName);

        var containerBuilder = new ContainerBuilder(database, containerName, "/partitionKey");

        // TODO: Build indexing policy here

        ContainerResponse? containerResponse = null;

        try
        {
            AsyncRetryPolicy? retryPolicy = Policy
                .Handle<Exception>()
                .WaitAndRetryAsync(5, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)) // exponential back-off: 2, 4, 8 etc, with jitter
                                                      + TimeSpan.FromMilliseconds(RandomUtil.Next(0, 1000)),
                    async (exception, timespan, retryCount) =>
                    {
                        _logger.LogError(exception, "*** CosmosSetupUtil *** Failed to EnsureContainer, trying again in {delay}s ... count: {retryCount}", timespan.Seconds, retryCount);
                        await ValueTask.CompletedTask;
                    });

            await retryPolicy.ExecuteAsync(async () =>
            {
                ThroughputProperties? containerThroughput = GetContainerThroughput(containerName);

                containerResponse = await containerBuilder.CreateIfNotExistsAsync(containerThroughput);

                _logger.LogDebug("Ensured container ({container})", containerName);
            });
        }
        catch (Exception e)
        {
            _logger.LogError(e, "*** CosmosSetupUtil *** Stopped retrying to create container ({container}), continuing...", containerName);
        }

        return containerResponse;
    }

    private ThroughputProperties? GetContainerThroughput(string containerName)
    {
        // TODO: Make container throughput here configurable

        ThroughputProperties? properties = null;

        _logger.LogDebug("Using AutoScale throughput for container ({containerName})", containerName);

        return properties;
    }
}