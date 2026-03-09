namespace ObservableOptions.Options;

public interface IEnumOption
{
    IEnumerable<string> Values { get; init; }
    object? SelectedValue { get; set; }
}