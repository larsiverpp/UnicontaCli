using NodaTime;
using Uniconta.ClientTools.DataModel;

namespace Liversen.UnicontaCli.Api.UcQuery;

sealed record InventoryTransaction(
    InventoryNumber InventoryNumber,

    LocalDate Date,

    // Seen types: Debtor, Creditor, Adjustment, IncludedInBOM, ReportAsFinished
    Uniconta.DataModel.InvMovementType MovementType)
{
    public static InventoryTransaction Create(InvTransClient value) =>
        new(
            InventoryNumber: new(value.Item),
            Date: new(value.Date.Year, value.Date.Month, value.Date.Day),
            MovementType: value.MovementTypeEnum);
}
