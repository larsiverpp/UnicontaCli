using Shouldly;
using Xunit;

namespace Liversen.UnicontaCli;

public static class IntegerValueTest
{
    [Fact]
    public static void GivenValue_WhenConstructing_ThenConstructed()
    {
        var value = ThreadLocalRandom.Next();

        var sut = new TestInteger(value);

        sut.Value.ShouldBe(value);
        (sut with { }).ShouldBe(sut);
    }

    [Fact]
    public static void GivenInstance_WhenConvertingToString_ThenAsString()
    {
        var value = ThreadLocalRandom.Next();
        new TestInteger(value).ToString().ShouldBe($"{value}");
    }

    [Theory]
    [InlineData(37, 42, int.MinValue, -1)]
    [InlineData(42, 42, 0, 0)]
    [InlineData(42, 37, 1, int.MaxValue)]
    public static void GivenTwoInstances_WhenComparing_ThenExpectedValue(int left, int right, int minExpected, int maxExpected) =>
        new TestInteger(left).CompareTo(new(right)).ShouldBeInRange(minExpected, maxExpected);

    sealed record TestInteger : IntegerValue<TestInteger>
    {
        public TestInteger(int value)
            : base(value)
        {
        }
    }
}
