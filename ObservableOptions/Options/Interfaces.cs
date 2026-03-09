namespace ObservableOptions.Options;

/// <summary>
/// Contract for enum-based options that expose their values as display strings.
/// </summary>
public interface IEnumOption
{
    /// <summary>Display names of all enum members.</summary>
    IEnumerable<string> Values { get; init; }

    /// <summary>Gets or sets the currently selected display name.</summary>
    object? SelectedValue { get; set; }

    /// <summary>Optional description or tooltip text.</summary>
    string? Description { get; init; }

    /// <summary>Display label for this option.</summary>
    string Text { get; init; }
}