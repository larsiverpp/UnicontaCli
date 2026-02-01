using System;
using System.Globalization;

namespace Liversen.StronglyTypedIds;

public record IntegerValue<TValue> : IIntegerValue, IComparable<TValue>
    where TValue : IntegerValue<TValue>
{
    public IntegerValue(int value)
    {
        Value = value;
    }

    public int Value { get; }

    public object ObjectValue() => Value;

    public sealed override string ToString() =>
        Value.ToString(CultureInfo.InvariantCulture);

    public int CompareTo(TValue? other) =>
        Value.CompareTo(other?.Value);
}
