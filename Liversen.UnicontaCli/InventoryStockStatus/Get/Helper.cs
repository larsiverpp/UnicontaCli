using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using NodaTime;
using Uniconta.DataModel;

namespace Liversen.UnicontaCli.InventoryStockStatus.Get;

class Helper
{
    public static readonly ImmutableArray<InvMovementType> IncludedMovementTypes =
    [
        InvMovementType.Creditor,
        InvMovementType.Debtor
    ];

    readonly Api.UcQuery.IClientSession ucClientSession;

    public Helper(Api.UcQuery.IClientSession ucClientSession)
    {
        this.ucClientSession = ucClientSession;
    }

    public async Task<ItemCollection> Get(LocalDate valueAt) =>
        await ucClientSession.WithClient<ItemCollection>(async ucClient =>
        {
            var statuses = await ucClient.GetInventoryStockStatuses(valueAt);
            var transactions = (await ucClient.GetInventoryTransactions(valueAt.Plus(-Period.FromYears(3))))
                .Where(x => IncludedMovementTypes.Contains(x.MovementType))
                .GroupBy(x => x.InventoryNumber)
                .ToImmutableDictionary(x => x.Key, x => x.ToImmutableArray());
            var items = statuses.Select(status =>
                {
                    var inventoryNumber = status.InventoryNumber;
                    var itemTransactions = transactions.TryGetValue(inventoryNumber, out var y) ? y : ImmutableArray<Api.UcQuery.InventoryTransaction>.Empty;
                    var mostRecentTransaction = itemTransactions.OrderByDescending(x => x.Date).FirstOrDefault();
                    var fraction = WriteDown.CalculateFraction(status, itemTransactions, valueAt);
                    return new Item(
                        InventoryNumber: inventoryNumber,
                        Name: status.Name,
                        Quantity: status.Quantity,
                        FullValue: status.CostValue,
                        LastMovement: mostRecentTransaction?.Date,
                        Fraction: fraction);
                })
                .OrderBy(x => x.InventoryNumber)
                .ToImmutableArray();
            return new(items);
        });
}
