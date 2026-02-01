using System.Threading.Tasks;
using NodaTime;

namespace Liversen.UnicontaCli.InventoryStockStatus.Get;

class Handler : ICommandHandler<Parameters>
{
    readonly Helper helper;
    readonly IConsole console;

    public Handler(
        Helper helper,
        IConsole console)
    {
        this.helper = helper;
        this.console = console;
    }

    public static string HeaderLine(LocalDate valueAt) =>
        $"=== InventoryStockStatus {LocalDateConverter.Serialize(valueAt)} ===";

    public async Task Run(Parameters parameters)
    {
        var collection = await helper.Get(parameters.ValueAt);
        var rows = collection.Rows(parameters.Culture);
        var consoleLines = parameters.CsvOutput ? ItemRow.ToCsvLines(rows) : ItemRow.ToScreenLines(rows);
        await console.Output.WriteLineAsync(HeaderLine(parameters.ValueAt));
        foreach (var screenLine in consoleLines)
        {
            await console.Output.WriteLineAsync(screenLine);
        }
    }
}
