using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Liversen.UnicontaCli.Test;

sealed class CommandHandler<TParameters> : ICommandHandler<TParameters>
{
    public TParameters? Parameters { get; private set; }

    public static void AddService(IServiceCollection collection)
    {
        collection.AddSingletonAsServiceAndSelf<ICommandHandler<TParameters>, CommandHandler<TParameters>>();
    }

    public Task Run(TParameters parameters)
    {
        Parameters = parameters;
        return Task.CompletedTask;
    }
}
