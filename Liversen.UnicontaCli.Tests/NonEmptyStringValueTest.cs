using System;
using Shouldly;
using Xunit;

namespace Liversen.UnicontaCli;

public static class NonEmptyStringValueTest
{
    [Fact]
    public static void GivenEmptyValue_WhenConstructing_ThenArgumentException() =>
        Should.Throw<ArgumentException>(() => new TestString(string.Empty));

    [Fact]
    public static void GivenWhitespaceValue_WhenConstructing_ThenArgumentException() =>
        Should.Throw<ArgumentException>(() => new TestString(" \t"));

    [Fact]
    public static void GivenInstance_WhenConvertingToString_ThenAsString()
    {
        var value = ThreadLocalRandom.NextUcAlphaOrDigitString(10);
        new TestString(value).ToString().ShouldBe(value);
    }

    [Theory]
    [InlineData("bar", "foo", int.MinValue, -1)]
    [InlineData("foo", "foo", 0, 0)]
    [InlineData("foo", "bar", 1, int.MaxValue)]
    public static void GivenTwoInstances_WhenComparing_ThenExpectedValue(string left, string right, int minExpected, int maxExpected) =>
        new TestString(left).CompareTo(new(right)).ShouldBeInRange(minExpected, maxExpected);

    sealed record TestString : NonEmptyStringValue<TestString>
    {
        public TestString(string value)
            : base(value)
        {
        }
    }
}
