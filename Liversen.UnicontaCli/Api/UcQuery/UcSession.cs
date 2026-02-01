using System;
using System.Threading.Tasks;
using Uniconta.API.Service;
using Uniconta.API.System;
using Uniconta.Common;
using Uniconta.Common.User;

namespace Liversen.UnicontaCli.Api.UcQuery;

sealed class UcSession : IAsyncDisposable
{
    UcSession(Session inner)
    {
        Inner = inner;
    }

    public Session Inner { get; }

    public static async Task<UcSession> Create(UcCredentials credentials)
    {
        var connection = new UnicontaConnection(APITarget.Live);
        var session = new Session(connection);
        VerifyNoError(await session.LoginAsync(
            credentials.LoginId,
            credentials.Password,
            LoginType.API,
            credentials.AccessIdentity));
        return new(session);
    }

    public async Task<QueryAPI> CreateQueryApi(CompanyId companyId)
    {
        var company = await Inner.GetCompany(companyId.Value);
        return company == null
            ? throw new ArgumentException($"GetCompany for id {companyId} failed with {Inner.LastError}")
            : new(Inner, company);
    }

    public async ValueTask DisposeAsync() =>
        await Inner.LogOut();

    static void VerifyNoError(ErrorCodes errorCode)
    {
        if (errorCode != ErrorCodes.Succes)
        {
            throw new ArgumentException(errorCode.ToString());
        }
    }
}
