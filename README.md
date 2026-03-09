# Iavory.ObservableOptions

I created ObservableOption for plugin development for  [Iavory](https://github.com/SilasMeyer4/Iavory). 
But the options can also be used for other purposes.

## Usage

### 1. Define your options class

```csharp
using ObservableOptions;

public class AppOptions : OptionsBase
{
    public Option<bool> IsLoggingEnabled { get; } = OptionFactory.Bool("Enable Logging", true);
    public Option<int> MaxRetries { get; } = OptionFactory.Int("Max Retries", 3);
    public Option<string> ApiUrl { get; } = OptionFactory.String("API URL", "https://example.com");
    public Option<LogLevel> LogLevel { get; } = OptionFactory.Enum("Log Level", LogLevel.Info);

    public AppOptions()
    {
        Initialize(); // required – registers all options
    }
}
```

### 2. Use the options

```csharp
var options = new AppOptions();

// Read a value
bool logging = options.IsLoggingEnabled.Value;

// Change a value
options.MaxRetries.Value = 5;

// React to changes
options.IsLoggingEnabled.ValueChanged += (s, e) =>
{
    Console.WriteLine($"Logging changed from {e.OldValue} to {e.NewValue}");
};

// Reset everything to defaults
options.ResetToDefaults();

// Get an option by key
var opt = options.GetByKey("MaxRetries");
```

### 3. Bind to UI (e.g. Avalonia / WPF)

Since every option implements `INotifyPropertyChanged`, you can bind directly to `Option.Value` in XAML:

```xml
<CheckBox IsChecked="{Binding Options.IsLoggingEnabled.Value}" />
```

## Available Factory Methods

| Method | Type |
|---|---|
| `OptionFactory.Bool(...)` | `Option<bool>` |
| `OptionFactory.Int(...)` | `Option<int>` |
| `OptionFactory.Float(...)` | `Option<float>` |
| `OptionFactory.Double(...)` | `Option<double>` |
| `OptionFactory.String(...)` | `Option<string>` |
| `OptionFactory.Enum<T>(...)` | `Option<TEnum>` |
| `OptionFactory.StringCollection(...)` | `CollectionOption<string>` |

## License

MIT – see [LICENSE](LICENSE)
