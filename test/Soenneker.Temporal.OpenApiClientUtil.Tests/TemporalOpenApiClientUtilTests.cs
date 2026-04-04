using Soenneker.Temporal.OpenApiClientUtil.Abstract;
using Soenneker.Tests.FixturedUnit;
using Xunit;

namespace Soenneker.Temporal.OpenApiClientUtil.Tests;

[Collection("Collection")]
public sealed class TemporalOpenApiClientUtilTests : FixturedUnitTest
{
    private readonly ITemporalOpenApiClientUtil _openapiclientutil;

    public TemporalOpenApiClientUtilTests(Fixture fixture, ITestOutputHelper output) : base(fixture, output)
    {
        _openapiclientutil = Resolve<ITemporalOpenApiClientUtil>(true);
    }

    [Fact]
    public void Default()
    {

    }
}
