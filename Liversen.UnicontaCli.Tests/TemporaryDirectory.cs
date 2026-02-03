using System;
using System.IO;

namespace Liversen.UnicontaCli;

public sealed class TemporaryDirectory : IDisposable
{
    public TemporaryDirectory()
    {
        Location = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        Directory.CreateDirectory(Location);
    }

    public string Location { get; }

    public void Dispose()
    {
        Directory.Delete(Location, true);
    }
}
