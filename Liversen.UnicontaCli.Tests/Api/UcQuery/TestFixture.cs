using System;
using System.Threading.Tasks;
using Uniconta.API.System;
using Xunit;

namespace Liversen.UnicontaCli.Api.UcQuery;

public sealed class TestFixture : IAsyncLifetime
{
    UcSession? session;
    QueryAPI? queryApi;

    public QueryAPI Api => queryApi ?? throw new InvalidOperationException("No API");

    public async Task InitializeAsync()
    {
        var (companyId, credentials) = TestData.CompanyIdCredentials();
        session = await UcSession.Create(credentials);
        queryApi = await session.CreateQueryApi(companyId);
    }

    public async Task DisposeAsync()
    {
        if (session != null)
        {
            await session.DisposeAsync();
        }
    }
}
