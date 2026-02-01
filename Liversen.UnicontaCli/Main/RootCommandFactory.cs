using System.CommandLine;

namespace Liversen.UnicontaCli.Main;

static class RootCommandFactory
{
    public static RootCommand Create(GlobalOptions globalOptions, IServiceProviderFactory serviceProviderFactory)
    {
        var rootCommand = new RootCommand("UnicontaCli");
        globalOptions.AddTo(rootCommand);
        rootCommand.Subcommands.Add(new InventoryStockStatus.Main.Command(serviceProviderFactory));
        return rootCommand;
    }
}
