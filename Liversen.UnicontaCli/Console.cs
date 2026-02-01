using System.IO;

namespace Liversen.UnicontaCli;

sealed class Console : IConsole
{
    public TextWriter Output => System.Console.Out;

    public TextWriter Error => System.Console.Error;
}
