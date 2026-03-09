using ObservableOptions.Options;

namespace ObservableOptions;

public sealed class OptionFactory
{
    public static Option<bool> Bool(string text, bool defaultValue, string? description = null)
    {
        return new CheckBoxOption()
        {
            Text = text,
            Default = defaultValue,
            Description = description,
            Value = defaultValue,
        };
    }
    
    public static Option<string> String(string text, string defaultValue, string? description = null)
    {
        return new TextOption()
        {
            Text = text,
            Default = defaultValue,
            Description = description,
            Value = defaultValue,
        };
    }

    public static Option<int> Int(string text, int defaultValue, string? description = null)
    {
        return new NumericIntOption()
        {
            Text = text,
            Default = defaultValue,
            Description = description,
            Value = defaultValue,
        }; 
    }
    
    public static Option<float> Float(string text, float defaultValue, string? description = null)
    {
        return new NumericFloatOption()
        {
            Text = text,
            Default = defaultValue,
            Description = description,
            Value = defaultValue,
        }; 
    }
    
    public static Option<double> Double(string text, double defaultValue, string? description = null)
    {
        return new NumericDoubleOption()
        {
            Text = text,
            Default = defaultValue,
            Description = description,
            Value = defaultValue,
        }; 
    }
    
    public static SliderOption Double(string text, double max, double min, double step, double startValue, string? description = null)
    {
        return new SliderOption()
        {
            Text = text,
            Default = startValue,
            Description = description,
            Value = startValue,
            Minimum = min,
            Maximum = max,
            Step = step,
        }; 
    }

    public static Option<TEnum> Enum<TEnum>(string text, TEnum defaultValue, string? description = null) where TEnum : struct, Enum
    {
        return new EnumOption<TEnum>()
        {
            Text = text,
            Default = defaultValue,
            Description = description,
            Value = defaultValue,
        };
    }
    
    public static CollectionOption<string> StringCollection(string text, IEnumerable<string> defaultValues, string? description = null)
    {
        return new StringListOption(defaultValues)
        {
            Text = text,
            Description = description,
        };
    }
}