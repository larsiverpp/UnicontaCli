using System.Globalization;
using NodaTime;

namespace Liversen.UnicontaCli.InventoryStockStatus.Get;

sealed record Parameters(
    LocalDate ValueAt,

    bool CsvOutput,

    CultureInfo Culture)
{
    public static Parameters Create(string valueAt, bool csvOutput, string cultureName) =>
        new(
            ValueAt: LocalDateConverter.ParseExtendedFormat(valueAt),
            CsvOutput: csvOutput,
            Culture: new(cultureName));
}
