using System;
using System.Threading.Tasks;

namespace Liversen.UnicontaCli.Api.UcQuery;

sealed class TestClientSession : IClientSession
{
    readonly TestClient client;

    public TestClientSession(TestClient client)
    {
        this.client = client;
    }

    public Task<T> WithClient<T>(Func<IClient, Task<T>> f) =>
        f(client);
}
