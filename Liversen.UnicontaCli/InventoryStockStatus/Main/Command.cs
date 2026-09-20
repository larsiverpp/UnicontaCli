namespace Liversen.UnicontaCli.InventoryStockStatus.Main;

sealed class Command : System.CommandLine.Command
{
    public const string CommandName = "inventory-stock-status";

    public Command(IServiceProviderFactory serviceProviderFactory)
        : base(CommandName, "Inventory stock status")
    {
        Subcommands.Add(new Get.Command(serviceProviderFactory));
    }
}
