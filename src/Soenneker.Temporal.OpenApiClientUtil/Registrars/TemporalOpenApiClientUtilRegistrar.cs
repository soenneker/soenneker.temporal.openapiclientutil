using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Soenneker.Temporal.HttpClients.Registrars;
using Soenneker.Temporal.OpenApiClientUtil.Abstract;

namespace Soenneker.Temporal.OpenApiClientUtil.Registrars;

/// <summary>
/// Registers the OpenAPI client utility for dependency injection.
/// </summary>
public static class TemporalOpenApiClientUtilRegistrar
{
    /// <summary>
    /// Adds <see cref="TemporalOpenApiClientUtil"/> as a singleton service. <para/>
    /// </summary>
    public static IServiceCollection AddTemporalOpenApiClientUtilAsSingleton(this IServiceCollection services)
    {
        services.AddTemporalOpenApiHttpClientAsSingleton()
                .TryAddSingleton<ITemporalOpenApiClientUtil, TemporalOpenApiClientUtil>();

        return services;
    }

    /// <summary>
    /// Adds <see cref="TemporalOpenApiClientUtil"/> as a scoped service. <para/>
    /// </summary>
    public static IServiceCollection AddTemporalOpenApiClientUtilAsScoped(this IServiceCollection services)
    {
        services.AddTemporalOpenApiHttpClientAsSingleton()
                .TryAddScoped<ITemporalOpenApiClientUtil, TemporalOpenApiClientUtil>();

        return services;
    }
}
