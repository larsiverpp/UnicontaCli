using System.CommandLine;
using Microsoft.Extensions.DependencyInjection;

namespace Liversen.UnicontaCli.InventoryStockStatus.Get;

class Command : System.CommandLine.Command
{
    public Command(IServiceProviderFactory serviceProviderFactory)
        : base(
            "get",
            """
            Gets inventory stock status at a given date.
            
            The status includes two values:
            - A full value identical to the value that can be seen in Uniconta.
            - A reduced value that is calculated by doing a write-down of items that have been in stock for a period with no movements.
            
            The write-down is done as follows:
            - If no movements have occurred for the last year, a write-down of 50% is done.
            - If no movements have occurred for the last two years, a write-down of 75% is done.
            - If no movements have occurred for the last three years, a write-down of 100% is done.
            
            Movement types considered are CREDITOR and DEBTOR.
            """)
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
