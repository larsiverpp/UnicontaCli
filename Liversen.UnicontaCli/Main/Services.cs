using Microsoft.Extensions.DependencyInjection;

namespace Liversen.UnicontaCli.Main;

static class Services
{
    public static void Add(IServiceCollection services)
    {
        services.AddSingleton<ICommandHandler<InventoryStockStatus.Get.Parameters>, InventoryStockStatus.Get.Handler>();
        services.AddSingleton<InventoryStockStatus.Get.Helper>();
    }
}
