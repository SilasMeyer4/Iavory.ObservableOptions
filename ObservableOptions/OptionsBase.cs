using System.Collections.ObjectModel;
using System.Reflection;

namespace ObservableOptions;

public class OptionsBase
{
    public ObservableCollection<OptionBase> AllOptions { get; } = new ObservableCollection<OptionBase>();

    protected void Initialize()
    {
        var properties = GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

        foreach (var property in properties)
        {
            if (!typeof(OptionBase).IsAssignableFrom(property.PropertyType)) continue;
            if (property.GetValue(this) is not OptionBase option) continue;

            SetKey(option, property.Name);
            AllOptions.Add(option);
        }

    }

    public void ResetToDefaults()
    {
        foreach (var option in AllOptions)
        {
            option.UntypedValue = option.UntypedDefault;
        }
    }
    
    public OptionBase? GetByKey(string key) => AllOptions.FirstOrDefault(o => o.Key == key);

    private static void SetKey(OptionBase option, string propertyName)
    {
        typeof(OptionBase).GetProperty(nameof(OptionBase.Key))!.SetValue(option, propertyName);
    }
}
