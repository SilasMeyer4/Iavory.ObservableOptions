using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace ObservableOptions.Options;

/// <summary>
/// An option with a numeric range, suitable for slider controls.
/// </summary>
public class SliderOption : Option<double>
{
    /// <summary>Minimum allowed value. Default is <c>0</c>.</summary>
    public double Minimum { get; set; } = 0;

    /// <summary>Maximum allowed value. Default is <c>100</c>.</summary>
    public double Maximum { get; set; } = 100;
}

/// <summary>An option holding a collection of strings.</summary>
public class StringCollectionOption(IEnumerable<string>? defaultItems = null) : CollectionOption<string>(defaultItems);

/// <summary>
/// A typed option that exposes all enum members as display strings and
/// synchronises <see cref="SelectedValue"/> with the underlying enum <see cref="Option{T}.Value"/>.
/// Respects <see cref="DisplayAttribute"/> on enum fields.
/// </summary>
/// <typeparam name="T">The enum type.</typeparam>
public class EnumOption<T> : Option<T>, IEnumOption where T : struct, Enum
{
    private readonly Dictionary<string, T> _nameToValue = new();
    public EnumOption()
    {
        _nameToValue = typeof(T).GetFields(BindingFlags.Public | BindingFlags.Static)
            .ToDictionary(
                f => f.GetCustomAttribute<DisplayAttribute>()?.Name ?? f.Name,
                f => (T)f.GetValue(null)!
                );
        Values = _nameToValue.Keys;
        SelectedValue = _nameToValue.Keys.FirstOrDefault();
    }

    /// <summary>Display names of all enum members, in declaration order.</summary>
    public IEnumerable<string> Values { get; init; }

    /// <summary>Gets or sets the current value. Wraps <see cref="SelectedValue"/>.</summary>
    public new T Value
    {
        get => (T)(SelectedValue ?? default(T));
        set => SelectedValue = value;
    }

    /// <summary>Gets or sets the selected display name. Setting it updates the underlying enum value.</summary>
    public object? SelectedValue
    {
        get
        {
            return _nameToValue.FirstOrDefault(kv => kv.Value.Equals(field)).Key;
        }
        set
        {
            if (value is string str && _nameToValue.TryGetValue(str, out var t))
            {
                var old = field;
                if (SetField(ref field, t))
                {
                    OnValueChanged((T)(old ?? default(T)), t);
                }
            }
        }
    }
}

/// <summary>A boolean option, typically rendered as a check box.</summary>
public class CheckBoxOption : Option<bool>;

/// <summary>A nullable boolean option, typically rendered as a three-state check box.</summary>
public class ThreeStateCheckBoxOption : Option<bool?>;

/// <summary>A string option, typically rendered as a text field.</summary>
public class TextOption : Option<string>;

/// <summary>An integer numeric option.</summary>
public class NumericIntOption : Option<int>;

/// <summary>A single-precision floating-point numeric option.</summary>
public class NumericFloatOption : Option<float>;

/// <summary>A double-precision floating-point numeric option.</summary>
public class NumericDoubleOption : Option<double>;

