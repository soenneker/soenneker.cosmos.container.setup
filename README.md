[![](https://img.shields.io/nuget/v/Soenneker.Cosmos.Container.Setup.svg?style=for-the-badge)](https://www.nuget.org/packages/Soenneker.Cosmos.Container.Setup/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.cosmos.container.setup/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.cosmos.container.setup/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/Soenneker.Cosmos.Container.Setup.svg?style=for-the-badge)](https://www.nuget.org/packages/Soenneker.Cosmos.Container.Setup/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.cosmos.container.setup/codeql.yml?label=CodeQL&style=for-the-badge)](https://github.com/soenneker/soenneker.cosmos.container.setup/actions/workflows/codeql.yml)

# Soenneker.Cosmos.Container.Setup

Ensures an Azure Cosmos DB container exists with the package's standard partition key path.

## Install

```bash
dotnet add package Soenneker.Cosmos.Container.Setup
```

## Registration

```csharp
using Soenneker.Cosmos.Container.Setup.Registrars;

services.AddCosmosContainerSetupUtilAsSingleton();
```

Use `AddCosmosContainerSetupUtilAsScoped()` when the setup service should follow a dependency-injection scope. Its Cosmos database dependency remains a long-lived singleton.

## Usage

Use configured Cosmos credentials and the configured default database:

```csharp
using Microsoft.Azure.Cosmos;
using Soenneker.Cosmos.Container.Setup.Abstract;

ContainerResponse? response = await setup.Ensure("orders", cancellationToken);
```

Or target an explicit account and database:

```csharp
ContainerResponse? response = await setup.Ensure(
    endpoint,
    accountKey,
    databaseName,
    containerName,
    cancellationToken);
```

An overload also accepts an existing `Microsoft.Azure.Cosmos.Database` handle.

## Provisioning behavior

- Containers are created with partition key path `/partitionKey`. The path is case-sensitive and must match the serialized document property exactly.
- No dedicated container throughput is configured; the service uses the database/account throughput behavior.
- Existing containers are returned by `CreateIfNotExistsAsync`; the utility does not reconcile partition keys, indexing policies, throughput, or other settings on an existing container.
- Transient Cosmos, HTTP, and timeout failures are retried up to five times with exponential delay and jitter. Authentication, authorization, and other non-transient Cosmos errors fail immediately.
- Cancellation stops the active SDK call and retry delays. After retries are exhausted, the final exception propagates to the caller.

`ContainerInfo` is a public metadata DTO for a container name, partition-key path, and composite indexes. The current `Ensure` methods do not consume it and always use `/partitionKey` with the SDK's default indexing policy.

Account keys are credentials. Store them in a secret provider and keep them out of logs and source control.
