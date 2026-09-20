using System;
using System.Globalization;

namespace Liversen.UnicontaCli;

public abstract record IntegerValue<TValue> : IComparable<TValue>
    where TValue : IntegerValue<TValue>
{
    protected IntegerValue(int value)
    {
        Value = value;
    }

    public int Value { get; }

    public sealed override string ToString() =>
        Value.ToString(CultureInfo.InvariantCulture);

    public int CompareTo(TValue? other) =>
        Value.CompareTo(other?.Value);
}
