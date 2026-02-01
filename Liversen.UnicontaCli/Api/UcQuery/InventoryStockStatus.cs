using System;
using NodaTime;
using Uniconta.ClientTools.DataModel;

namespace Liversen.UnicontaCli.Api.UcQuery;

record InventoryStockStatus(
    InventoryNumber InventoryNumber,

    string Name,

    LocalDate Date,

    decimal Quantity,

    decimal CostValue)
{
    public static InventoryStockStatus Create(InvStockStatus value) =>
        new(
            new(value.Item),
            Name: value.ItemName,
            new(value.Date.Year, value.Date.Month, value.Date.Day),
            Convert.ToDecimal(value.Qty),
            Convert.ToDecimal(value.CostValue));
}
