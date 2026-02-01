namespace Liversen.UnicontaCli.InventoryStockStatus.Main;

class Command : System.CommandLine.Command
{
    public Command(IServiceProviderFactory serviceProviderFactory)
        : base("inventory-stock-status", "Inventory stock status")
    {
        Subcommands.Add(new Get.Command(serviceProviderFactory));
    }
}
