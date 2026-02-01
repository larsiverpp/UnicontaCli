using System;
using System.CommandLine;

namespace Liversen.UnicontaCli;

interface IServiceProviderFactory
{
    IServiceProvider Create(ParseResult parseResult);
}
