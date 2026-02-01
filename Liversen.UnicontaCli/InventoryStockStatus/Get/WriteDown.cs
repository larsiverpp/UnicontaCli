using System.Collections.Immutable;
using System.Linq;
using NodaTime;

namespace Liversen.UnicontaCli.InventoryStockStatus.Get;

static class WriteDown
{
    public static decimal CalculateFraction(
        Api.UcQuery.InventoryStockStatus status,
        ImmutableArray<Api.UcQuery.InventoryTransaction> transactions,
        LocalDate valueAt)
    {
        var mostRecent = transactions.OrderByDescending(x => x.Date).FirstOrDefault();
        if (mostRecent == null)
        {
            return 0;
        }

        var mostRecentDate = mostRecent.Date;
        if (mostRecentDate > valueAt.Minus(Period.FromYears(1)))
        {
            return 1;
        }

        if (mostRecentDate > valueAt.Minus(Period.FromYears(2)))
        {
            return 0.5M;
        }

        if (mostRecentDate > valueAt.Minus(Period.FromYears(3)))
        {
            return 0.25M;
        }

        return 0;
    }
}
