using System;
using System.Collections.Immutable;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using NodaTime;
using Uniconta.API.System;
using Uniconta.ClientTools.DataModel;
using Uniconta.Common;

namespace Liversen.UnicontaCli.Api.UcQuery;

class Client : IClient
{
    readonly QueryAPI inner;

    public Client(QueryAPI inner)
    {
        this.inner = inner;
    }

    public async Task<ImmutableArray<InventoryStockStatus>> GetInventoryStockStatuses(LocalDate toDate, int take = int.MaxValue)
    {
        var value = ToString(toDate);
        var items = await inner.Query<InvStockStatus>(
        [
            PropValuePair.GenereteParameter(
                "ToDate",
                typeof(DateTime),
                value)
        ]);
        return [.. items.Take(take).Select(InventoryStockStatus.Create)];
    }

    public async Task<ImmutableArray<InventoryTransaction>> GetInventoryTransactions(LocalDate minDate, int take = int.MaxValue)
    {
        var items = await inner.Query<InvTransClient>(
        [
            PropValuePair.GenereteWhereElements(
                "Date",
                LocalDateConverter.ToDateTime(minDate),
                CompareOperator.GreaterThanOrEqual)
        ]);
        return [.. items.Where(x => !string.IsNullOrWhiteSpace(x.Item)).Take(take).Select(InventoryTransaction.Create)];
    }

    static string ToString(LocalDate value) =>
        value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
}
