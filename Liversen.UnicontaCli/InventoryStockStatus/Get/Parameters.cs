using System.Globalization;
using NodaTime;

namespace Liversen.UnicontaCli.InventoryStockStatus.Get;

sealed record Parameters(
    LocalDate ValueAt,

    bool CsvOutput,

    string? OutputPath,

    CultureInfo Culture)
{
    public static Parameters Create(
        string valueAt,
        bool csvOutput,
        string? outputPath,
        string cultureName) =>
        new(
            ValueAt: LocalDateConverter.ParseExtendedFormat(valueAt),
            CsvOutput: csvOutput,
            OutputPath: outputPath,
            Culture: new(cultureName));
}
