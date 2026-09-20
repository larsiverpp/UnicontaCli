namespace Liversen.UnicontaCli.Api.UcQuery;

sealed record InventoryNumber : NonEmptyStringValue<InventoryNumber>
{
    public InventoryNumber(string value)
        : base(value)
    {
    }
}
