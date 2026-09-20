using System;
using System.Globalization;
using NodaTime;

namespace Liversen.UnicontaCli;

static class LocalDateConverter
{
    public const string ExtendedFormat = "yyyy-MM-dd";

    public static DateTime ToDateTime(LocalDate value) =>
        new(
            year: value.Year,
            month: value.Month,
            day: value.Day,
            hour: 0,
            minute: 0,
            second: 0,
            kind: DateTimeKind.Unspecified);

    public static LocalDate ParseExtendedFormat(string value) =>
        LocalDate.FromDateTime(DateTime.ParseExact(value, ExtendedFormat, CultureInfo.InvariantCulture));

    public static string Serialize(LocalDate value) =>
        value.ToString(ExtendedFormat, CultureInfo.InvariantCulture);
}
