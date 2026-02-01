using System.Linq;
using System.Threading.Tasks;
using NodaTime;
using Shouldly;
using Xunit;

namespace Liversen.UnicontaCli.Api.UcQuery;

public class TestClientTest
{
    readonly TestClient sut = new();
    readonly LocalDate today = UnicontaCli.TestData.RandomLocalDate();

    [Fact]
    public async Task GivenStatusesForToday_WhenGettingStatusesForToday_ThenStatuses()
    {
        var statuses = UnicontaCli.TestData.RandomArray(() =>
            sut.AddStatus(
                TestData.RandomInventoryStockStatus(date: today)));

        var response = await sut.GetInventoryStockStatuses(today);

        response.ShouldBe(statuses, ignoreOrder: true);
    }

    [Fact]
    public async Task GivenStatuses_WhenGettingStatusesInThePast_ThenStatusesInThePast()
    {
        var statuses = UnicontaCli.TestData.RandomArray(i =>
            sut.AddStatus(
                TestData.RandomInventoryStockStatus(date: today.PlusDays(-i))));

        var response = await sut.GetInventoryStockStatuses(today.PlusDays(-1));

        response.ShouldBe(statuses.Skip(1), ignoreOrder: true);
    }

    [Fact]
    public async Task GivenTransactions_WhenGettingTransactionsSomeTimeAgo_ThenSomeTransactions()
    {
        var transactions = UnicontaCli.TestData.RandomArray(i =>
            sut.AddTransaction(
                TestData.RandomInventoryTransaction(date: today.PlusDays(i))));

        var response = await sut.GetInventoryTransactions(today.PlusDays(1));

        response.ShouldBe(transactions.Skip(1), ignoreOrder: true);
    }

    [Fact]
    public async Task GivenTransactionsForToday_WhenGettingTransactionsFromToday_ThenTransactions()
    {
        var transactions = UnicontaCli.TestData.RandomArray(() =>
            sut.AddTransaction(
                TestData.RandomInventoryTransaction(date: today)));

        var response = await sut.GetInventoryTransactions(today.PlusDays(-7));

        response.ShouldBe(transactions, ignoreOrder: true);
    }
}
