using System;
using System.Threading.Tasks;

namespace Liversen.UnicontaCli.Api.UcQuery;

class ClientSession : IClientSession
{
    readonly UcCredentials credentials;
    readonly CompanyId companyId;

    public ClientSession(UcCredentials credentials, CompanyId companyId)
    {
        this.credentials = credentials;
        this.companyId = companyId;
    }

    public async Task<T> WithClient<T>(Func<IClient, Task<T>> f)
    {
        await using var session = await UcSession.Create(credentials);
        var queryApi = await session.CreateQueryApi(companyId);
        var client = new Client(queryApi);
        return await f(client);
    }
}
