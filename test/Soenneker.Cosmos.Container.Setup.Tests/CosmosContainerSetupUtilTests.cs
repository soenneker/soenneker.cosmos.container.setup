using Soenneker.Cosmos.Container.Setup.Abstract;
using Soenneker.Tests.HostedUnit;

namespace Soenneker.Cosmos.Container.Setup.Tests;

[ClassDataSource<Host>(Shared = SharedType.PerTestSession)]
public class CosmosContainerSetupUtilTests : HostedUnitTest
{
    private readonly ICosmosContainerSetupUtil _util;

    public CosmosContainerSetupUtilTests(Host host) : base(host)
    {
        _util = Resolve<ICosmosContainerSetupUtil>(true);
    }

    [Test]
    public void Default()
    {

    }
}
