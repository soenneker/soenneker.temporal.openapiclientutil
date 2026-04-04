using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Kiota.Http.HttpClientLibrary;
using Soenneker.Extensions.Configuration;
using Soenneker.Extensions.ValueTask;
using Soenneker.Temporal.HttpClients.Abstract;
using Soenneker.Temporal.OpenApiClientUtil.Abstract;
using Soenneker.Temporal.OpenApiClient;
using Soenneker.Kiota.GenericAuthenticationProvider;
using Soenneker.Utils.AsyncSingleton;

namespace Soenneker.Temporal.OpenApiClientUtil;

///<inheritdoc cref="ITemporalOpenApiClientUtil"/>
public sealed class TemporalOpenApiClientUtil : ITemporalOpenApiClientUtil
{
    private readonly AsyncSingleton<TemporalOpenApiClient> _client;

    public TemporalOpenApiClientUtil(ITemporalOpenApiHttpClient httpClientUtil, IConfiguration configuration)
    {
        _client = new AsyncSingleton<TemporalOpenApiClient>(async token =>
        {
            HttpClient httpClient = await httpClientUtil.Get(token).NoSync();

            var apiKey = configuration.GetValueStrict<string>("Temporal:ApiKey");
            string authHeaderValueTemplate = configuration["Temporal:AuthHeaderValueTemplate"] ?? "{token}";
            string authHeaderValue = authHeaderValueTemplate.Replace("{token}", apiKey, StringComparison.Ordinal);

            var requestAdapter = new HttpClientRequestAdapter(new GenericAuthenticationProvider(headerValue: authHeaderValue), httpClient: httpClient);

            return new TemporalOpenApiClient(requestAdapter);
        });
    }

    public ValueTask<TemporalOpenApiClient> Get(CancellationToken cancellationToken = default)
    {
        return _client.Get(cancellationToken);
    }

    public void Dispose()
    {
        _client.Dispose();
    }

    public ValueTask DisposeAsync()
    {
        return _client.DisposeAsync();
    }
}
