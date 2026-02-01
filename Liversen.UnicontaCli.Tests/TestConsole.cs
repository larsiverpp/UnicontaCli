using System;
using System.IO;

namespace Liversen.UnicontaCli;

sealed class TestConsole : IConsole, IDisposable
{
    public StringWriter OutputInner { get; } = new();

    public StringWriter ErrorInner { get; } = new();

    TextWriter IConsole.Output => OutputInner;

    TextWriter IConsole.Error => ErrorInner;

    public void Dispose()
    {
        OutputInner.Dispose();
        ErrorInner.Dispose();
    }
}
