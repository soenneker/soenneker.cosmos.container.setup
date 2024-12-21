using Soenneker.Cosmos.Container.Setup.Abstract;
using Soenneker.Tests.FixturedUnit;
using Xunit;


namespace Soenneker.Cosmos.Container.Setup.Tests;

[Collection("Collection")]
public class CosmosContainerSetupUtilTests : FixturedUnitTest
{
    private readonly ICosmosContainerSetupUtil _util;

    public CosmosContainerSetupUtilTests(Fixture fixture, ITestOutputHelper output) : base(fixture, output)
    {
        _util = Resolve<ICosmosContainerSetupUtil>(true);
    }

    [Fact]
    public void Default()
    {

    }
}
