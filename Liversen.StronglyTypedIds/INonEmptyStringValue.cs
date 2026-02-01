namespace Liversen.StronglyTypedIds;

public interface INonEmptyStringValue : ISingleValue
{
    string Value { get; }
}
