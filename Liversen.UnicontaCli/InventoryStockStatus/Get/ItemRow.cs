using System.Collections.Immutable;
using System.Linq;

namespace Liversen.UnicontaCli.InventoryStockStatus.Get;

public record ItemRow(
    string InventoryNumber,

    string Name,

    string Quantity,

    string FullValue,

    string LastMovement,

    string Fraction,

    string ReducedValue)
{
    public const int CellCount = 7;

    public ImmutableArray<string> Cells =>
        [InventoryNumber, Name, Quantity, FullValue, LastMovement, Fraction, ReducedValue];

    public string CsvLine =>
        string.Join('\t', Cells);

    public static ImmutableArray<string> ToScreenLines(ImmutableArray<ItemRow> rows)
    {
        var widths = Enumerable.Repeat(0, CellCount).ToArray();
        foreach (var cells in rows.Select(x => x.Cells))
        {
            for (var i = 0; i < CellCount; ++i)
            {
                widths[i] = int.Max(widths[i], cells[i].Length);
            }
        }

        return [..rows.Select(x => $"{x.InventoryNumber.PadRight(widths[0])} {x.Name.PadRight(widths[1])} {x.Quantity.PadLeft(widths[2])} {x.FullValue.PadLeft(widths[3])} {x.LastMovement.PadLeft(widths[4])} {x.Fraction.PadLeft(widths[5])} {x.ReducedValue.PadLeft(widths[6])}")];
    }

    public static ImmutableArray<string> ToCsvLines(ImmutableArray<ItemRow> rows) =>
        [..rows.Select(x => x.CsvLine)];
}
