using System.Collections.ObjectModel;
using Microsoft.Azure.Cosmos;

namespace Soenneker.Cosmos.Container.Setup.Dtos;

public class ContainerInfo
{
    /// <summary>
    /// Container Name
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Container partition Key
    /// </summary>
    public string PartitionKeyPath { get; }

    public Collection<Collection<CompositePath>> CompositeIndexes { get; set; }

    public ContainerInfo(string name, string partitionKeyPath)
    {
        CompositeIndexes = new Collection<Collection<CompositePath>>();
        Name = name;
        PartitionKeyPath = partitionKeyPath;
    }
}