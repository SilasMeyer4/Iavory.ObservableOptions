using System.ComponentModel.DataAnnotations;
using Avalonia.Controls;
using ObservableOptions;
using ObservableOptions.Options;

namespace GeneratorTestApp.Models;

public enum LogLevel{
    [Display(Name = "Error")]
    Error,
    [Display(Name = "Warning")]
    Warning,
    [Display(Name = "Info display Test")]
    Info,
    [Display(Name = "Verbose")]
    Verbose
}

public class TestOptions : OptionsBase
{
    public Option<bool> IsLoggingEnabled { get; } = OptionFactory.Bool("IsLoggingEnabled", true);
    public Option<LogLevel> LogLevel { get; } = OptionFactory.Enum("LogLevel", Models.LogLevel.Info);
    public Option<bool?> ThreeState { get; } = OptionFactory.ThreeState("ThreeState", null);
    public NumericDoubleOption NumericDouble { get; } = OptionFactory.Double("NumericDouble", 20);
    public NumericIntOption NumericInt { get; } = OptionFactory.Int("NumericInt", 20);
    public NumericFloatOption NumericFloat { get; } = OptionFactory.Float("NumericFloat", 20);
    public SliderOption Slider { get; } = OptionFactory.Slider("NumericDouble", 0, 100, 1);
    public TextOption Text { get; } = OptionFactory.String("Text", "TestText");
    public StringCollectionOption StringCollection { get; } =  OptionFactory.StringCollection("StringCollection Test", 
        ["Test1", "Text2", "Text3414"]);
    
    
    public TestOptions()
    {
        Initialize();
        IsLoggingEnabled.ValueChanged += LoggingChanged;
    }

    private void LoggingChanged(object? sender, OptionValueChangedEventArgs<bool> e)
    {
       
    }
}