using System.Collections.Immutable;
using System.CommandLine;
using System.Threading.Tasks;

namespace Liversen.UnicontaCli.Main;

static class Program
{
    internal static Task<int> Main(string[] args) =>
        Execute([.. args], new Console());

    internal static async Task<int> Execute(ImmutableArray<string> args, IConsole console)
    {
        var globalOptions = new GlobalOptions();
        var serviceProviderFactory = new ServiceProviderFactory(globalOptions, console);
        var rootCommand = RootCommandFactory.Create(globalOptions, serviceProviderFactory);
        var parseResult = rootCommand.Parse(args);

        var invocationConfiguration = new InvocationConfiguration
        {
            Output = console.Output,
            Error = console.Error
        };

        return await parseResult.InvokeAsync(invocationConfiguration);
    }
}
