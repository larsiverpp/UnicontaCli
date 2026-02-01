using System.Collections.Immutable;
using System.Globalization;
using System.Linq;
using Shouldly;
using Xunit;

namespace Liversen.UnicontaCli.InventoryStockStatus.Get;

public static class ItemCollectionTest
{
    static readonly ImmutableArray<Item> Items = [
        new(
            InventoryNumber: new("AB123"),
            Name: TestData.RandomName(),
            Quantity: 37M,
            FullValue: 42122,
            LastMovement: null,
            Fraction: 0.5M),
        new(
            InventoryNumber: new("AB234"),
            Name: TestData.RandomName(),
            Quantity: 57M,
            FullValue: 24,
            LastMovement: null,
            Fraction: 0.25M),
        new(
            InventoryNumber: new("AB345"),
            Name: TestData.RandomName(),
            Quantity: 73M,
            FullValue: 37,
            LastMovement: null,
            Fraction: 0)
    ];

    static readonly CultureInfo TestCulture = CultureInfo.InvariantCulture;

    [Fact]
    public static void GivenCollection_WhenGettingFullValue_ThenFullValue() =>
        new ItemCollection(Items).FullValue.ShouldBe(42183);

    [Fact]
    public static void GivenCollection_WhenGettingReducedValue_ThenReducedValue() =>
        new ItemCollection(Items).ReducedValue.ShouldBe(21067);

    [Fact]
    public static void GivenCollection_WhenGettingRows_ThenRows()
    {
        var collection = new ItemCollection(Items);
        collection.Rows(TestCulture).ShouldBe(
        [
            ItemCollection.HeaderRow,
            ..Items.Select(x => x.ToItemRow(TestCulture)),
            collection.FooterRow(TestCulture)
        ]);
    }

    [Fact]
    public static void GivenCollection_WhenGettingFooterRow_ThenFooterRow() =>
        new ItemCollection(Items).FooterRow(TestCulture).ShouldBe(
            new(
                InventoryNumber: ItemCollection.TotalsLabel,
                Name: string.Empty,
                Quantity: string.Empty,
                FullValue: "42,183.00",
                LastMovement: string.Empty,
                Fraction: string.Empty,
                ReducedValue: "21,067.00"));
}
