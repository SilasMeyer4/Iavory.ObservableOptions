namespace ObservableOptions;

public sealed class OptionFactory
{
    public static Option<bool> Bool(string text, bool defaultValue, string? description = null)
    {
        return new Option<bool>()
        {
            Text = text,
            Default = defaultValue,
            Description = description,
            Value = defaultValue,
        };
    }
    
    public static Option<string> String(string text, string defaultValue, string? description = null)
    {
        return new Option<string>()
        {
            Text = text,
            Default = defaultValue,
            Description = description,
            Value = defaultValue,
        };
    }

    public static Option<int> Int(string text, int defaultValue, string? description = null)
    {
        return new Option<int>()
        {
            Text = text,
            Default = defaultValue,
            Description = description,
            Value = defaultValue,
        }; 
    }
    
    public static Option<float> Float(string text, float defaultValue, string? description = null)
    {
        return new Option<float>()
        {
            Text = text,
            Default = defaultValue,
            Description = description,
            Value = defaultValue,
        }; 
    }
    
    public static Option<double> Double(string text, double defaultValue, string? description = null)
    {
        return new Option<double>()
        {
            Text = text,
            Default = defaultValue,
            Description = description,
            Value = defaultValue,
        }; 
    }

    public static Option<TEnum> Enum<TEnum>(string text, TEnum defaultValue, string? description = null) where TEnum : struct, Enum
    {
        return new Option<TEnum>()
        {
            Text = text,
            Default = defaultValue,
            Description = description,
            Value = defaultValue,
        };
    }
    
    public static CollectionOption<string> StringCollection(string text, IEnumerable<string> defaultValues, string? description = null)
    {
        return new CollectionOption<string>(defaultValues)
        {
            Text = text,
            Description = description,
        };
    }
}