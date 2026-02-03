using System.Collections.Immutable;
using System.IO;
using System.Text;
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
        var lines = parameters.CsvOutput ? ItemRow.ToCsvLines(rows) : ItemRow.ToScreenLines(rows);
        if (string.IsNullOrEmpty(parameters.OutputPath))
        {
            await WriteLines(console.Output, parameters.ValueAt, lines);
        }
        else
        {
            await using var stream = File.Open(parameters.OutputPath, FileMode.Create, FileAccess.Write, FileShare.None);
            await using var writer = new StreamWriter(stream, new UTF8Encoding(true));
            await WriteLines(writer, parameters.ValueAt, lines);
        }
    }

    static async Task WriteLines(TextWriter writer, LocalDate valueAt, ImmutableArray<string> lines)
    {
        await writer.WriteLineAsync(HeaderLine(valueAt));
        foreach (var line in lines)
        {
            await writer.WriteLineAsync(line);
        }
    }
}
