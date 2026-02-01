using System;
using System.Globalization;
using NodaTime;

namespace Liversen.UnicontaCli;

static class LocalDateConverter
{
    public const string ExtendedFormat = "yyyy-MM-dd";

    public static DateTime ToDateTime(LocalDate value) =>
        new(
            value.Year,
            value.Month,
            value.Day,
            0,
            0,
            0,
            DateTimeKind.Unspecified);

    public static LocalDate ParseExtendedFormat(string value) =>
        LocalDate.FromDateTime(DateTime.ParseExact(value, ExtendedFormat, CultureInfo.InvariantCulture));

    public static string Serialize(LocalDate value) =>
        value.ToString(ExtendedFormat, CultureInfo.InvariantCulture);
}
