using System;
using Shouldly;
using Xunit;

namespace Liversen.UnicontaCli;

public static class LocalDateConverterTest
{
    [Fact]
    public static void GivenLocalDate_WhenConvertingToDateTime_ThenDateTime() =>
        LocalDateConverter.ToDateTime(new(2026, 9, 26))
            .ShouldBe(new(
                year: 2026,
                month: 9,
                day: 26,
                hour: 0,
                minute: 0,
                second: 0,
                kind: DateTimeKind.Unspecified));
}
