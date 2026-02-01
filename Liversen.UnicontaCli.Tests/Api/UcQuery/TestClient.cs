using System.Collections.Concurrent;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using NodaTime;

namespace Liversen.UnicontaCli.Api.UcQuery;

sealed class TestClient : IClient
{
    readonly ConcurrentBag<InventoryStockStatus> statuses = [];
    readonly ConcurrentBag<InventoryTransaction> transactions = [];

    public Task<ImmutableArray<InventoryStockStatus>> GetInventoryStockStatuses(LocalDate toDate, int take = int.MaxValue) =>
        Task.FromResult(
            statuses
                .GroupBy(x => x.InventoryNumber)
                .Select(x => x.Where(y => y.Date <= toDate).OrderByDescending(y => y.Date).FirstOrDefault())
                .WhereNotNull()
                .ToImmutableArray());

    public Task<ImmutableArray<InventoryTransaction>> GetInventoryTransactions(LocalDate minDate, int take = int.MaxValue) =>
        Task.FromResult(
            transactions
                .Where(x => x.Date >= minDate)
                .ToImmutableArray());

    public InventoryStockStatus AddStatus(InventoryStockStatus? status = null)
    {
        status ??= TestData.RandomInventoryStockStatus();
        statuses.Add(status);
        return status;
    }

    public InventoryTransaction AddTransaction(InventoryTransaction? transaction = null)
    {
        transaction ??= TestData.RandomInventoryTransaction();
        transactions.Add(transaction);
        return transaction;
    }
}
