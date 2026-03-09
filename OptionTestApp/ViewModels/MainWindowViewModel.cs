using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;
using GeneratorTestApp.Models;
using ObservableOptions.Options;

namespace GeneratorTestApp.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{

    private TestOptions _options = new TestOptions();
    public ObservableCollection<object> OptionEntries { get; } = new();

    public MainWindowViewModel()
    {
        foreach (var optionsAllOption in _options.AllOptions)
        {
            OptionEntries.Add(optionsAllOption);
        }
    }

    [RelayCommand]
    private void AddItem(object? parameter)
    {
        if (parameter is not ReadOnlyCollection<object> items || items.Count < 2)
            return;

        if (items[0] is not StringCollectionOption option)
            return;

        var text = items[1] as string;
        if (string.IsNullOrWhiteSpace(text))
            return;

        option.Collection.Add(text);
    }

    [RelayCommand]
    private void RemoveListEntry(StringCollectionOption? textListOptionEntry)
    {
        if (textListOptionEntry is null || textListOptionEntry.SelectedItem is null) return;
        
        textListOptionEntry.Remove(textListOptionEntry.SelectedItem);
    }
}