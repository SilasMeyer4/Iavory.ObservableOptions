using ObservableOptions;

namespace GeneratorTestApp.Models;

public enum LogLevel{
    Error,
    Warning,
    Info,
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