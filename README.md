[![](https://img.shields.io/nuget/v/soenneker.temporal.openapiclientutil.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.temporal.openapiclientutil/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.temporal.openapiclientutil/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.temporal.openapiclientutil/actions/workflows/publish-package.yml)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.temporal.openapiclientutil/codeql.yml?label=CodeQL&style=for-the-badge)](https://github.com/soenneker/soenneker.temporal.openapiclientutil/actions/workflows/codeql.yml)
[![](https://img.shields.io/nuget/dt/soenneker.temporal.openapiclientutil.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.temporal.openapiclientutil/)

# ![](https://user-images.githubusercontent.com/4441470/224455560-91ed3ee7-f510-4041-a8d2-3fc093025112.png) Soenneker.Temporal.OpenApiClientUtil
Provides lazily initialized, cached access to Temporal's generated OpenAPI client.

## Installation

```bash
dotnet add package Soenneker.Temporal.OpenApiClientUtil
```

## Configuration

```json
{
  "Temporal": {
    "ClientBaseUrl": "https://your-namespace.your-account.tmprl.cloud/",
    "ApiKey": "your-api-key"
  }
}
```

## Registration

```csharp
using Soenneker.Temporal.OpenApiClientUtil.Registrars;

services.AddTemporalOpenApiClientUtilAsScoped();
```

Scoped registration lets the generated-client wrapper follow the current scope while its authenticated HTTP provider remains singleton. Use `AddTemporalOpenApiClientUtilAsSingleton()` when the wrapper itself should be application-wide.

## Usage

```csharp
using Soenneker.Temporal.OpenApiClient;
using Soenneker.Temporal.OpenApiClient.Models;
using Soenneker.Temporal.OpenApiClientUtil.Abstract;

public sealed class TemporalService
{
    private readonly ITemporalOpenApiClientUtil _clients;

    public TemporalService(ITemporalOpenApiClientUtil clients)
    {
        _clients = clients;
    }

    public async ValueTask<GetSystemInfoResponse?> GetSystemInfo(CancellationToken cancellationToken)
    {
        TemporalOpenApiClient client = await _clients.Get(cancellationToken);
        return await client.Api.V1.SystemInfo.GetAsync(cancellationToken: cancellationToken);
    }
}
```

Do not dispose the generated client returned by `Get()`. The utility owns its cached wrapper, while the registered HTTP provider owns the underlying `HttpClient`.
