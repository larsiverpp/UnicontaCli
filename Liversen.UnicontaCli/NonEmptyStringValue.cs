using System;

namespace Liversen.UnicontaCli;

public abstract record NonEmptyStringValue<TValue> : IComparable<TValue>
    where TValue : NonEmptyStringValue<TValue>
{
    protected NonEmptyStringValue(string value)
    {
        Value = value;
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentNullException(nameof(value));
        }
    }

    public string Value { get; }

    public sealed override string ToString() =>
        Value;

    public int CompareTo(TValue? other) =>
        string.CompareOrdinal(Value, other?.Value);
}
