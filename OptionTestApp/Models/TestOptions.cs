using System.ComponentModel.DataAnnotations;
using ObservableOptions;

namespace GeneratorTestApp.Models;

public enum LogLevel{
    [Display(Name = "Error")]
    Error,
    [Display(Name = "Warning")]
    Warning,
    [Display(Name = "Info")]
    Info,
    [Display(Name = "Verbose")]
    Verbose
}

public class TestOptions : OptionsBase
{
    public Option<bool> IsLoggingEnabled = OptionFactory.Bool("IsLoggingEnabled", true);
    public Option<LogLevel> LogLevel = OptionFactory.Enum("LogLevel", Models.LogLevel.Info);
    
    public TestOptions()
    {
        IsLoggingEnabled.ValueChanged += LoggingChanged;
    }

    private void LoggingChanged(object? sender, OptionValueChangedEventArgs<bool> e)
    {
        throw new System.NotImplementedException();
    }
}