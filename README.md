[![](https://img.shields.io/nuget/v/Soenneker.Cosmos.Container.Setup.svg?style=for-the-badge)](https://www.nuget.org/packages/Soenneker.Cosmos.Container.Setup/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.cosmos.container.setup/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.cosmos.container.setup/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/Soenneker.Cosmos.Container.Setup.svg?style=for-the-badge)](https://www.nuget.org/packages/Soenneker.Cosmos.Container.Setup/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.cosmos.container.setup/codeql.yml?label=CodeQL&style=for-the-badge)](https://github.com/soenneker/soenneker.cosmos.container.setup/actions/workflows/codeql.yml)

# Soenneker.Cosmos.Container.Setup

Singleton.

## Install

```bash
dotnet add package Soenneker.Cosmos.Container.Setup
```

## Quick start

```csharp
using Soenneker.Cosmos.Container.Setup.Registrars;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();
var result = services.AddCosmosContainerSetupUtilAsSingleton();
```

Registers Cosmos Container Setup Util with a singleton lifetime.

## What you get

- `ICosmosContainerSetupUtil` — Singleton.
- `CosmosContainerSetupUtilRegistrar` — A utility library for Azure Cosmos container setup operations.
- `ContainerInfo` — Represents the container info.

## API at a glance

| API | What it does | Result / important behavior |
| --- | --- | --- |
| `ICosmosContainerSetupUtil.Ensure(database, containerName, cancellationToken)` | Ensures cosmos Container Setup. | A task whose result is the requested container Response. |
| `CosmosContainerSetupUtilRegistrar.AddCosmosContainerSetupUtilAsSingleton(services)` | Registers Cosmos Container Setup Util with a singleton lifetime. | The same service collection, so additional registrations can be chained. |
| `CosmosContainerSetupUtilRegistrar.AddCosmosContainerSetupUtilAsScoped(services)` | Registers Cosmos Container Setup Util with a scoped lifetime. | The same service collection, so additional registrations can be chained. |
| `ContainerInfo.Name` | Container Name. | Container Name. |
| `ContainerInfo.PartitionKeyPath` | Container partition Key. | Container partition Key. |
| `ContainerInfo.CompositeIndexes` | Gets or sets composite indexes. | Gets or sets composite indexes. |

## Practical notes

- Cancellation stops pending work; it does not undo work that has already completed.
- Calls that return a cached or singleton value reuse the same instance until the owning service is disposed.
