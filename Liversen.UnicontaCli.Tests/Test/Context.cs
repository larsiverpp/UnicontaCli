using System;
using Microsoft.Extensions.DependencyInjection;

namespace Liversen.UnicontaCli.Test;

public abstract class Context : IDisposable
{
    readonly ServiceProvider serviceProvider;

    protected Context(Func<IServiceCollection> servicesFactory, Action<IServiceCollection>? servicesAdder = null)
    {
        var serviceCollection = servicesFactory.Invoke();
        servicesAdder?.Invoke(serviceCollection);
        serviceProvider = serviceCollection.BuildServiceProvider();
    }

    internal string ConsoleOutput => Resolve<IConsole>().Output.ToString()!;

    internal IServiceProvider ServiceProvider => serviceProvider;

    public T Resolve<T>()
        where T : notnull =>
        serviceProvider.GetRequiredService<T>();

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            serviceProvider.Dispose();
        }
    }
}
