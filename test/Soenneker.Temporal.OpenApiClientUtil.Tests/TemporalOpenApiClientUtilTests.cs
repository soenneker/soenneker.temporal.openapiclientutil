using Soenneker.Temporal.OpenApiClientUtil.Abstract;
using Soenneker.Tests.HostedUnit;

namespace Soenneker.Temporal.OpenApiClientUtil.Tests;

[ClassDataSource<Host>(Shared = SharedType.PerTestSession)]
public sealed class TemporalOpenApiClientUtilTests : HostedUnitTest
{
    private readonly ITemporalOpenApiClientUtil _openapiclientutil;

    public TemporalOpenApiClientUtilTests(Host host) : base(host)
    {
        _openapiclientutil = Resolve<ITemporalOpenApiClientUtil>(true);
    }

    [Test]
    public void Default()
    {

    }
}
