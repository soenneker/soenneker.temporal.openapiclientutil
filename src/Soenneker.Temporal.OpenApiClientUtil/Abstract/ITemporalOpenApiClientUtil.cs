using Soenneker.Temporal.OpenApiClient;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Temporal.OpenApiClientUtil.Abstract;

/// <summary>
/// Exposes a cached OpenAPI client instance.
/// </summary>
public interface ITemporalOpenApiClientUtil: IDisposable, IAsyncDisposable
{
    ValueTask<TemporalOpenApiClient> Get(CancellationToken cancellationToken = default);
}
