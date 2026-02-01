using System;

namespace Liversen.StronglyTypedIds;

public record NonEmptyStringValue<TValue> : INonEmptyStringValue, IComparable<TValue>
    where TValue : NonEmptyStringValue<TValue>
{
    public NonEmptyStringValue(string value)
    {
        Value = value;
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentNullException(nameof(value));
        }
    }

    public string Value { get; }

    public object ObjectValue() => Value;

    public sealed override string ToString() =>
        Value;

    public int CompareTo(TValue? other) =>
        string.CompareOrdinal(Value, other?.Value);
}
