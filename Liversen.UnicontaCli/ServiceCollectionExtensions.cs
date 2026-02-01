using Microsoft.Extensions.DependencyInjection;

namespace Liversen.UnicontaCli;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddSingletonAsServiceAndSelf<TService, TImplementation>(
        this IServiceCollection services)
        where TService : class
        where TImplementation : class, TService =>
        services
            .AddSingleton<TImplementation>()
            .AddSingleton<TService>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
}
