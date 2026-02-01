using System.Collections.Generic;
using System.Collections.Immutable;
using System.Globalization;
using System.Linq;

namespace Liversen.UnicontaCli.InventoryStockStatus.Get;

record ItemCollection(
    ImmutableArray<Item> Items)
{
    public const string TotalsLabel = "=== TOTALS ===";

    public static readonly ItemRow HeaderRow = new(
        InventoryNumber: "InventoryNumber",
        Name: "Name",
        Quantity: "Quantity",
        FullValue: "FullValue",
        LastMovement: "LastMovement",
        Fraction: "Fraction",
        ReducedValue: "ReducedValue");

    public decimal FullValue =>
        Items.Sum(line => line.FullValue);

    public decimal ReducedValue =>
        Items.Sum(line => line.ReducedValue);

    public ImmutableArray<ItemRow> Rows(CultureInfo cultureInfo) =>
    [
        HeaderRow,
        ..ItemRows(cultureInfo),
        FooterRow(cultureInfo)
    ];

    public IEnumerable<ItemRow> ItemRows(CultureInfo cultureInfo) =>
        Items.Select(x => x.ToItemRow(cultureInfo));

    public ItemRow FooterRow(CultureInfo cultureInfo) =>
        new(
                InventoryNumber: TotalsLabel,
                Name: string.Empty,
                Quantity: string.Empty,
                FullValue: Item.SerializeAmount(FullValue, cultureInfo),
                LastMovement: string.Empty,
                Fraction: string.Empty,
                ReducedValue: Item.SerializeAmount(ReducedValue, cultureInfo));
}
