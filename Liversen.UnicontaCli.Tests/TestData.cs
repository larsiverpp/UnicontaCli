using System;
using System.Collections.Immutable;
using System.Linq;
using NodaTime;

namespace Liversen.UnicontaCli;

static class TestData
{
    public const int DefaultCount = 3;
    public const int RandomLocalDateRangeDaysCount = 365 * 8000;
    public static readonly LocalDate RandomLocalDateMin = new(1900, 1, 1);

    public static LocalDate RandomLocalDate() =>
        RandomLocalDateMin.PlusDays(ThreadLocalRandom.Next(RandomLocalDateRangeDaysCount));

    public static ImmutableArray<T> RandomArray<T>(Func<T> generator, int? count = null) =>
        Enumerable.Range(0, count ?? DefaultCount).Aggregate(ImmutableArray<T>.Empty, (x, _) => x.Add(generator()));

    public static ImmutableArray<T> RandomArray<T>(Func<int, T> generator, int? count = null) =>
        Enumerable.Range(0, count ?? DefaultCount).Aggregate(ImmutableArray<T>.Empty, (x, i) => x.Add(generator(i)));

    public static T RandomArrayValue<T>(params ImmutableArray<T> values) =>
        values[ThreadLocalRandom.Next(values.Length)];

    public static TEnum RandomEnumValue<TEnum>()
        where TEnum : struct, Enum =>
        Enum.GetValues<TEnum>()[ThreadLocalRandom.Next(Enum.GetValues<TEnum>().Length)];

    public static string RandomName() =>
        ThreadLocalRandom.NextUcAlphaString(10);
}
