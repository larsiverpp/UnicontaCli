using System.Globalization;
using NodaTime;

namespace Liversen.UnicontaCli.InventoryStockStatus.Get;

record Item(
    Api.UcQuery.InventoryNumber InventoryNumber,

    string Name,

    decimal Quantity,

    decimal FullValue,

    LocalDate? LastMovement,

    decimal Fraction)
{
    public decimal ReducedValue =>
        decimal.Round(FullValue * Fraction, 2);

    public ItemRow ToItemRow(CultureInfo cultureInfo) =>
        new(
            InventoryNumber: InventoryNumber.Value,
            Name: TruncateName(Name),
            Quantity: Quantity.ToString(cultureInfo),
            FullValue: SerializeAmount(FullValue, cultureInfo),
            LastMovement: LastMovement != null ? LocalDateConverter.Serialize(LastMovement.Value) : string.Empty,
            Fraction: SerializeAmount(Fraction, cultureInfo),
            ReducedValue: SerializeAmount(ReducedValue, cultureInfo));

    public static string TruncateName(string value)
    {
        var firstLine = value.ReplaceLineEndings("\n").Split('\n')[0];
        return firstLine[..int.Min(50, firstLine.Length)];
    }

    public static string SerializeAmount(decimal value, CultureInfo cultureInfo) =>
        value.ToString("#,##0.00", cultureInfo);
}
