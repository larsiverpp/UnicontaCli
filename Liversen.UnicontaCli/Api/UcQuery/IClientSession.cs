using System;
using System.Threading.Tasks;

namespace Liversen.UnicontaCli.Api.UcQuery;

interface IClientSession
{
    public Task<T> WithClient<T>(Func<IClient, Task<T>> f);
}
