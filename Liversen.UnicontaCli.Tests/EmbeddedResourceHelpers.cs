using System;
using System.IO;
using System.Reflection;

namespace Liversen.UnicontaCli;

static class EmbeddedResourceHelpers
{
    public static Stream OpenStream(string resourceLocalName) =>
        OpenStream(
            Assembly.GetExecutingAssembly(),
            $"{typeof(EmbeddedResourceHelpers).Namespace}.{resourceLocalName}");

    public static Stream OpenStream(Assembly assembly, string resourceName) =>
        assembly.GetManifestResourceStream(resourceName)
        ?? throw new ArgumentException($"Unable to find resource {resourceName} in assembly {assembly.FullName}");
}
