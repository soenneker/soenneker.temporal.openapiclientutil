using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Kiota.Abstractions.Authentication;
using Microsoft.Kiota.Http.HttpClientLibrary;
using Soenneker.Extensions.ValueTask;
using Soenneker.Temporal.HttpClients.Abstract;
using Soenneker.Temporal.OpenApiClientUtil.Abstract;
using Soenneker.Temporal.OpenApiClient;
using Soenneker.Utils.AsyncSingleton;

namespace Soenneker.Temporal.OpenApiClientUtil;

public sealed class TemporalOpenApiClientUtil : ITemporalOpenApiClientUtil
{
    private readonly AsyncSingleton<TemporalOpenApiClient> _client;

    public TemporalOpenApiClientUtil(ITemporalOpenApiHttpClient httpClientUtil)
    {
        _client = new AsyncSingleton<TemporalOpenApiClient>(async token =>
        {
            HttpClient httpClient = await httpClientUtil.Get(token).NoSync();

            var requestAdapter = new HttpClientRequestAdapter(new AnonymousAuthenticationProvider(), httpClient: httpClient);

            return new TemporalOpenApiClient(requestAdapter);
        });
    }

    public ValueTask<TemporalOpenApiClient> Get(CancellationToken cancellationToken = default)
    {
        return _client.Get(cancellationToken);
    }

    /// <summary>
    /// Releases resources used by the current instance.
    /// </summary>
    public void Dispose()
    {
        _client.Dispose();
    }

    /// <summary>
    /// Asynchronously releases resources used by the current instance.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public ValueTask DisposeAsync()
    {
        return _client.DisposeAsync();
    }
}
