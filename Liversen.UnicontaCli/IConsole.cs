using System.IO;

namespace Liversen.UnicontaCli;

interface IConsole
{
    TextWriter Output { get; }

    TextWriter Error { get; }
}
