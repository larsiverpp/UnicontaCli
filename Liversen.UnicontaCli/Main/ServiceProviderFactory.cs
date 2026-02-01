using System;
using System.CommandLine;
using Microsoft.Extensions.DependencyInjection;

namespace Liversen.UnicontaCli.Main;

sealed class ServiceProviderFactory : IServiceProviderFactory
{
    readonly GlobalOptions globalOptions;
    readonly IConsole console;

    public ServiceProviderFactory(GlobalOptions globalOptions, IConsole console)
    {
        this.globalOptions = globalOptions;
        this.console = console;
    }

    public IServiceProvider Create(ParseResult parseResult)
    {
        var loginId = parseResult.GetRequiredValue(globalOptions.LoginId);
        var password = parseResult.GetRequiredValue(globalOptions.Password);
        var accessIdentity = parseResult.GetRequiredValue(globalOptions.AccessIdentity);
        var companyId = parseResult.GetRequiredValue(globalOptions.CompanyId);
        var clientSession = new Api.UcQuery.ClientSession(new(loginId, password, accessIdentity), new(companyId));

        var services = new ServiceCollection();
        Services.Add(services);
        services.AddSingleton(console);
        services.AddSingleton<Api.UcQuery.IClientSession>(clientSession);

        return services.BuildServiceProvider();
    }
}
