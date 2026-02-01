using System.Collections.Immutable;
using System.Threading.Tasks;
using NodaTime;

namespace Liversen.UnicontaCli.Api.UcQuery;

interface IClient
{
    Task<ImmutableArray<InventoryStockStatus>> GetInventoryStockStatuses(LocalDate toDate, int take = int.MaxValue);

    Task<ImmutableArray<InventoryTransaction>> GetInventoryTransactions(LocalDate minDate, int take = int.MaxValue);
}
