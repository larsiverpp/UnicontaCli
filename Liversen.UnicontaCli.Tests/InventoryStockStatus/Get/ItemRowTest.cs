using Shouldly;
using Xunit;

namespace Liversen.UnicontaCli.InventoryStockStatus.Get;

public static class ItemRowTest
{
    [Fact]
    public static void GivenRow_WhenConvertingToCsvLine_ThenCsvLine() =>
        new ItemRow(
                InventoryNumber: "AB123",
                Name: "Foobar",
                Quantity: "37",
                FullValue: "42.00",
                LastMovement: "2025-12-31",
                Fraction: "0.50",
                ReducedValue: "21.00").CsvLine
            .ShouldBe("AB123\tFoobar\t37\t42.00\t2025-12-31\t0.50\t21.00");

    [Fact]
    public static void GivenRow_WhenConvertingToScreenLines_ThenScreenLine()
    {
        var row1 = new ItemRow(
            InventoryNumber: "AB123",
            Name: "Foobar",
            Quantity: "137",
            FullValue: "442.00",
            LastMovement: string.Empty,
            Fraction: "0.50",
            ReducedValue: "221.00");
        var row2 = new ItemRow(
            InventoryNumber: "AB1234",
            Name: "Bar foo",
            Quantity: "37",
            FullValue: "42.00",
            LastMovement: "2025-12-31",
            Fraction: "1.00",
            ReducedValue: "42.00");
        ItemRow.ToScreenLines([row1, row2])
            .ShouldBe([
                "AB123  Foobar  137 442.00            0.50 221.00",
                "AB1234 Bar foo  37  42.00 2025-12-31 1.00  42.00"
            ]);
    }
}
