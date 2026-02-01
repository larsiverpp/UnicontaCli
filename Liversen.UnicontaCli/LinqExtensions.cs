using System.Collections.Generic;
using System.Linq;

namespace Liversen.UnicontaCli;

public static class LinqExtensions
{
    public static IEnumerable<T> WhereNotNull<T>(this IEnumerable<T?> input)
        where T : notnull =>
        input.Where(x => x is not null).Select(x => x!);
}
