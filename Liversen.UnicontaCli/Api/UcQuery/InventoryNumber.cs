using Liversen.StronglyTypedIds;

namespace Liversen.UnicontaCli.Api.UcQuery;

record InventoryNumber : NonEmptyStringValue<InventoryNumber>
{
    public InventoryNumber(string value)
        : base(value)
    {
    }
}
