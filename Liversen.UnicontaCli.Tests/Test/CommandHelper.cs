using System;
using System.CommandLine;

namespace Liversen.UnicontaCli.Test;

sealed class CommandHelper
{
    public static RootCommand CreateRootCommand(IServiceProvider serviceProvider) =>
        Main.RootCommandFactory.Create(new(), new TestServiceProvider(serviceProvider));

    sealed class TestServiceProvider : IServiceProviderFactory
    {
        readonly IServiceProvider serviceProvider;

        public TestServiceProvider(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public IServiceProvider Create(ParseResult parseResult) =>
            serviceProvider;
    }
}
