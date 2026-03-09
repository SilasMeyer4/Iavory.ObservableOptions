using System.Diagnostics.CodeAnalysis;
using ObservableOptions.Options;

namespace ObservableOptions;

/// <summary>
/// Factory for creating pre-configured <see cref="OptionBase"/> instances.
/// </summary>
public sealed class OptionFactory
{
    /// <summary>Creates a <see cref="CheckBoxOption"/> with the given default value.</summary>
    public static CheckBoxOption Bool(string text, bool defaultValue, string? description = null)
    {
        return new CheckBoxOption()
        {
            Text = text,
            Default = defaultValue,
            Description = description,
            Value = defaultValue,
        };
    }

    /// <summary>Creates a <see cref="ThreeStateCheckBoxOption"/> with the given default value.</summary>
    public static ThreeStateCheckBoxOption ThreeState(string text, bool? defaultValue, string? description = null)
    {
        return new ThreeStateCheckBoxOption()
        {
            Text = text,
            Default = defaultValue,
            Description = description,
            Value = defaultValue,
        };
    }

    /// <summary>Creates a <see cref="TextOption"/> with the given default value.</summary>
    public static TextOption String(string text, string defaultValue, string? description = null)
    {
        return new TextOption()
        {
            Text = text,
            Default = defaultValue,
            Description = description,
            Value = defaultValue,
        };
    }

    /// <summary>Creates a <see cref="NumericIntOption"/> with the given default value.</summary>
    public static NumericIntOption Int(string text, int defaultValue, string? description = null)
    {
        return new NumericIntOption()
        {
            Text = text,
            Default = defaultValue,
            Description = description,
            Value = defaultValue,
        };
    }

    /// <summary>Creates a <see cref="NumericFloatOption"/> with the given default value.</summary>
    public static NumericFloatOption Float(string text, float defaultValue, string? description = null)
    {
        return new NumericFloatOption()
        {
            Text = text,
            Default = defaultValue,
            Description = description,
            Value = defaultValue,
        };
    }

    /// <summary>Creates a <see cref="NumericDoubleOption"/> with the given default value.</summary>
    public static NumericDoubleOption Double(string text, double defaultValue, string? description = null)
    {
        return new NumericDoubleOption()
        {
            Text = text,
            Default = defaultValue,
            Description = description,
            Value = defaultValue,
        };
    }

    /// <summary>Creates a <see cref="SliderOption"/> with the given range and start value.</summary>
    /// <param name="min">Minimum allowed value.</param>
    /// <param name="max">Maximum allowed value.</param>
    /// <param name="startValue">Initial and default value.</param>
    public static SliderOption Slider(string text, double min, double max, double startValue, string? description = null)
    {
        return new SliderOption()
        {
            Text = text,
            Default = startValue,
            Description = description,
            Value = startValue,
            Minimum = min,
            Maximum = max,
        };
    }

    /// <summary>Creates an <see cref="EnumOption{TEnum}"/> with the given default enum value.</summary>
    public static EnumOption<TEnum> Enum<TEnum>(string text, TEnum defaultValue, string? description = null) where TEnum : struct, Enum
    {
        return new EnumOption<TEnum>()
        {
            Text = text,
            Default = defaultValue,
            Description = description,
            Value = defaultValue,
        };
    }

    /// <summary>Creates a <see cref="StringCollectionOption"/> with the given default items.</summary>
    public static StringCollectionOption StringCollection(string text, IEnumerable<string> defaultValues, string? description = null)
    {
        return new StringCollectionOption(defaultValues)
        {
            Text = text,
            Description = description,
        };
    }
}