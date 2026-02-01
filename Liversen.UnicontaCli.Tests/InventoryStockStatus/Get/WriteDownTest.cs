using System.Collections.Immutable;
using NodaTime;
using Shouldly;
using Xunit;

namespace Liversen.UnicontaCli.InventoryStockStatus.Get;

public static class WriteDownTest
{
    [Theory]
    [InlineData(0, 800, 400, 1)]
    [InlineData(800, 400, 364, 1)]
    [InlineData(400, 365, 800, 0.5)]
    [InlineData(900, 731, 800, 0.25)]
    [InlineData(4567, 1234, 1097, 0.0)]
    public static void GivenTransactions_WhenCalculating_ThenFraction(
        int daysBefore1,
        int daysBefore2,
        int daysBefore3,
        decimal fraction)
    {
        var valueAt = new LocalDate(2025, 12, 31);
        var status = Api.UcQuery.TestData.RandomInventoryStockStatus();
        var inventoryNumber = status.InventoryNumber;
        var transactions = ImmutableArray.Create(
            Api.UcQuery.TestData.RandomInventoryTransaction(inventoryNumber, valueAt.PlusDays(-daysBefore1)),
            Api.UcQuery.TestData.RandomInventoryTransaction(inventoryNumber, valueAt.PlusDays(-daysBefore2)),
            Api.UcQuery.TestData.RandomInventoryTransaction(inventoryNumber, valueAt.PlusDays(-daysBefore3)));

        var v = WriteDown.CalculateFraction(status, transactions, valueAt);

        v.ShouldBe(fraction);
    }

    [Fact]
    public static void GivenNoTransactions_WhenCalculating_ThenZero()
    {
        var valueAt = TestData.RandomLocalDate();
        var status = Api.UcQuery.TestData.RandomInventoryStockStatus();

        var v = WriteDown.CalculateFraction(status, [], valueAt);

        v.ShouldBe(0);
    }
}
