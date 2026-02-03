using System.CommandLine;
using Microsoft.Extensions.DependencyInjection;

namespace Liversen.UnicontaCli.InventoryStockStatus.Get;

class Command : System.CommandLine.Command
{
    public Command(IServiceProviderFactory serviceProviderFactory)
        : base("get", "Get inventory stock status")
    {
        var valueAtArgument = new Argument<string>("valueAt")
        {
            Description = "Date to get the inventory stock status at (format: yyyy-MM-dd)"
        };
        Add(valueAtArgument);
        var csvOutputOption = new Option<bool>("--csv")
        {
            Description = "Output as CSV file separated by tabs"
        };
        Add(csvOutputOption);
        var outputPathOption = new Option<string>("--outputPath")
        {
            Description = "Ouput to file instead of console"
        };
        Add(outputPathOption);
        var cultureNameOption = new Option<string>("--culture")
        {
            Description = "Culture name for formatting (format: see Locale Code at https://simplelocalize.io/data/locales)",
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
                        outputPath: parseResult.GetValue(outputPathOption),
                        cultureName: parseResult.GetRequiredValue(cultureNameOption)));
        });
    }
}
