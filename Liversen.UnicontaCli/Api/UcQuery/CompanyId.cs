using Liversen.StronglyTypedIds;

namespace Liversen.UnicontaCli.Api.UcQuery;

record CompanyId : IntegerValue<CompanyId>
{
    public CompanyId(int value)
        : base(value)
    {
    }
}
