namespace ObservableOptions.Options;

public class EnumDisplayAttribute(string? value) : Attribute
{
    public string? Value { get; init; } = value;
}