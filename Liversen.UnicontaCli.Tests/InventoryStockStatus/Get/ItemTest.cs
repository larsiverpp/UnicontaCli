using System.Globalization;
using Shouldly;
using Xunit;

namespace Liversen.UnicontaCli.InventoryStockStatus.Get;

public static class ItemTest
{
    [Fact]
    public static void GivenItemWithFractionOne_WhenGettingReducedValue_ThenFullValue() =>
        new Item(
                InventoryNumber: Api.UcQuery.TestData.RandomInventoryNumber(),
                Name: TestData.RandomName(),
                Quantity: ThreadLocalRandom.NextDecimal(),
                FullValue: 42,
                LastMovement: null,
                Fraction: 1)
            .ReducedValue.ShouldBe(42);

    [Fact]
    public static void GivenItemWithFractionHalf_WhenGettingReducedValue_ThenHalfValue() =>
        new Item(
                InventoryNumber: Api.UcQuery.TestData.RandomInventoryNumber(),
                Name: TestData.RandomName(),
                Quantity: ThreadLocalRandom.NextDecimal(),
                FullValue: 42,
                LastMovement: null,
                Fraction: 0.5M)
            .ReducedValue.ShouldBe(21);

    [Fact]
    public static void GivenItemWithFractionZero_WhenGettingReducedValue_ThenZeroValue() =>
        new Item(
                InventoryNumber: Api.UcQuery.TestData.RandomInventoryNumber(),
                Name: TestData.RandomName(),
                Quantity: ThreadLocalRandom.NextDecimal(),
                FullValue: 42,
                LastMovement: null,
                Fraction: 0)
            .ReducedValue.ShouldBe(0);

    [Fact]
    public static void GivenItem_WhenConvertingToItemRow_ThenItemRow() =>
        new Item(
                InventoryNumber: new("AB123"),
                Name: "Foobars",
                Quantity: 37M,
                FullValue: 42122,
                LastMovement: new(2025, 12, 31),
                Fraction: 0.5M)
            .ToItemRow(CultureInfo.InvariantCulture)
            .ShouldBe(new(
                InventoryNumber: "AB123",
                Name: "Foobars",
                Quantity: "37",
                FullValue: "42,122.00",
                LastMovement: "2025-12-31",
                Fraction: "0.50",
                ReducedValue: "21,061.00"));

    [Theory]
    [InlineData("01234567890123456789012345678901234567890123456789", "01234567890123456789012345678901234567890123456789")]
    [InlineData("012345678901234567890123456789012345678901234567890", "01234567890123456789012345678901234567890123456789")]
    [InlineData("foo\nbar", "foo")]
    [InlineData("foo\r\nbar", "foo")]
    [InlineData("0123456789012345678901234567890123456789012345678901\r\nbar", "01234567890123456789012345678901234567890123456789")]
    public static void GivenName_WhenTruncating_ThenName(string input, string expected) =>
        Item.TruncateName(input).ShouldBe(expected);
}
