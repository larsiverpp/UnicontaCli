namespace Liversen.UnicontaCli.Api.UcQuery;

sealed record CompanyId : IntegerValue<CompanyId>
{
    public CompanyId(int value)
        : base(value)
    {
    }
}
