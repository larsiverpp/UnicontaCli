using System.CommandLine;
using Microsoft.Extensions.DependencyInjection;

namespace Liversen.UnicontaCli.InventoryStockStatus.Get;

class Command : System.CommandLine.Command
{
    public Command(IServiceProviderFactory serviceProviderFactory)
        : base("get", "Get inventory stock status")
    {
        var valueAtArgument = new Argument<string>("valueAt");
        Add(valueAtArgument);
        var csvOutputOption = new Option<bool>("--csv");
        Add(csvOutputOption);
        var cultureNameOption = new Option<string>("--culture")
        {
            DefaultValueFactory = _ => "da-DK"
        };
        Add(cultureNameOption);

        SetAction(async parseResult =>
        {
            await serviceProviderFactory.Create(parseResult)
                .GetRequiredService<ICommandHandler<Parameters>>()
                .Run(
                    Get.Parameters.Create(
                        valueAt: parseResult.GetRequiredValue(valueAtArgument),
                        csvOutput: parseResult.GetValue(csvOutputOption),
                        cultureName: parseResult.GetRequiredValue(cultureNameOption)));
        });
    }
}
