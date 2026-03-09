using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace ObservableOptions.Options;

public class SliderOptions : Option<float>
{
    public double Minimum { get; set; } = 0;
    public double Maximum { get; set; } = 100;
    public double Step { get; set; } = 1;
}

public class StringListOption : CollectionOption<string>;

public class EnumOption<T> : Option<T>, IEnumOption where T : struct, Enum
{
    public EnumOption()
    {
        Values = typeof(T).GetFields(BindingFlags.Public | BindingFlags.Static)
            .Select(f => f.GetCustomAttribute<DisplayAttribute>()?.Name ?? f.Name);
    }

    public IEnumerable<string> Values { get; init; }

    public object? SelectedValue
    {
        get => Value;
        set
        {
            if (value is T t)
            {
                Value = t;
            }
        }
    }
}

public class CheckBoxOption : Option<bool>;

public class TreeStateCheckBoxOption : Option<bool?>;