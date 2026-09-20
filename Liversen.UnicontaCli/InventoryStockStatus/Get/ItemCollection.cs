using System.Collections.Generic;
using System.Collections.Immutable;
using System.Globalization;
using System.Linq;
using NodaTime;

namespace Liversen.UnicontaCli.InventoryStockStatus.Get;

sealed record ItemCollection(
    ImmutableArray<Item> Items)
{
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

    public static string TotalsLabel(LocalDate valueAt) =>
        $"TOTALS [{LocalDateConverter.Serialize(valueAt)}]";

    public ImmutableArray<ItemRow> Rows(CultureInfo cultureInfo, LocalDate valueAt) =>
    [
        HeaderRow,
        ..ItemRows(cultureInfo),
        FooterRow(cultureInfo, valueAt)
    ];

    public IEnumerable<ItemRow> ItemRows(CultureInfo cultureInfo) =>
        Items.Select(x => x.ToItemRow(cultureInfo));

    public ItemRow FooterRow(CultureInfo cultureInfo, LocalDate valueAt) =>
        new(
                InventoryNumber: TotalsLabel(valueAt),
                Name: string.Empty,
                Quantity: string.Empty,
                FullValue: Item.SerializeAmount(FullValue, cultureInfo),
                LastMovement: string.Empty,
                Fraction: string.Empty,
                ReducedValue: Item.SerializeAmount(ReducedValue, cultureInfo));
}
