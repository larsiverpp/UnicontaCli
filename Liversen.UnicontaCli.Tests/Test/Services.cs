using System.Globalization;
using Microsoft.Extensions.DependencyInjection;

namespace Liversen.UnicontaCli.Test;

static class Services
{
    public static ServiceCollection CreateServices()
    {
        var services = new ServiceCollection();
        Add(services);
        return services;
    }

    static void Add(IServiceCollection services)
    {
        Main.Services.Add(services);
        services.AddSingletonAsServiceAndSelf<Api.UcQuery.IClient, Api.UcQuery.TestClient>();
        services.AddSingletonAsServiceAndSelf<Api.UcQuery.IClientSession, Api.UcQuery.TestClientSession>();
        services.AddSingletonAsServiceAndSelf<IConsole, TestConsole>();
        services.AddSingleton(CultureInfo.InvariantCulture);
    }
}
