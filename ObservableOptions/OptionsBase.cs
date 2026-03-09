using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reflection;

namespace ObservableOptions;

/// <summary>
/// Base class for option containers. Collects all <see cref="OptionBase"/> properties and
/// forwards their <see cref="INotifyPropertyChanged"/> events.
/// </summary>
public class OptionsBase : INotifyPropertyChanged
{
    /// <inheritdoc/>
    public event PropertyChangedEventHandler? PropertyChanged;

    /// <summary>All options registered in this container, in declaration order.</summary>
    public ObservableCollection<OptionBase> AllOptions { get; } = new ObservableCollection<OptionBase>();

    /// <summary>
    /// Discovers all public <see cref="OptionBase"/> properties, assigns their keys and
    /// registers them in <see cref="AllOptions"/>. Call this at the end of the constructor.
    /// </summary>
    protected void Initialize()
    {
        var properties = GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

        foreach (var property in properties)
        {
            if (!typeof(OptionBase).IsAssignableFrom(property.PropertyType)) continue;
            if (property.GetValue(this) is not OptionBase option) continue;

            SetKey(option, property.Name);
            AllOptions.Add(option);
            option.PropertyChanged += (_, _) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(option.Key));
        }
    }

    /// <summary>Resets every option to its default value.</summary>
    public void ResetToDefaults()
    {
        foreach (var option in AllOptions)
        {
            option.UntypedValue = option.UntypedDefault;
        }
    }

    /// <summary>Returns the option with the given <paramref name="key"/>, or <see langword="null"/> if not found.</summary>
    public OptionBase? GetByKey(string key) => AllOptions.FirstOrDefault(o => o.Key == key);

    private static void SetKey(OptionBase option, string propertyName)
    {
        typeof(OptionBase).GetProperty(nameof(OptionBase.Key))!.SetValue(option, propertyName);
    }
}
