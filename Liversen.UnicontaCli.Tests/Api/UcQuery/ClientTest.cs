using System.Threading.Tasks;
using NodaTime;
using Shouldly;
using Xunit;

namespace Liversen.UnicontaCli.Api.UcQuery;

public class ClientTest : IClassFixture<TestFixture>
{
    readonly Client sut;
    readonly LocalDate today = SystemClock.Instance.GetCurrentInstant().InUtc().Date;

    public ClientTest(TestFixture fixture)
    {
        sut = new(fixture.Api);
    }

    [Fact]
    [Trait("Category", TestCategory.Unstable)]
    public async Task GivenStatuses_WhenGettingStatusesForToday_ThenSomeStatuses()
    {
        var rows = await sut.GetInventoryStockStatuses(today, take: 3);

        rows.ShouldNotBeEmpty();
    }

    [Fact]
    [Trait("Category", TestCategory.Unstable)]
    public async Task GivenTransactions_WhenGettingTransactionsInThePast_ThenSomeTransactions()
    {
        var rows = await sut.GetInventoryTransactions(today.PlusDays(-30), take: 3);

        rows.ShouldNotBeEmpty();
    }
}
