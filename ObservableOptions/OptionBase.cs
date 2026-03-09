using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ObservableOptions;

/// <summary>
/// Abstract base class for all options. Implements <see cref="INotifyPropertyChanged"/>.
/// </summary>
public abstract class OptionBase : INotifyPropertyChanged
{
    /// <summary>The unique key of this option, derived from the property name in <see cref="OptionsBase"/>.</summary>
    public string Key { get; internal set; } = string.Empty;

    /// <summary>Display label for this option.</summary>
    public string Text { get; init; } = string.Empty;

    /// <summary>Optional description or tooltip text.</summary>
    public string? Description { get; init; } = string.Empty;

    /// <summary>Gets or sets the current value as an untyped object.</summary>
    public abstract object? UntypedValue { get; set; }

    /// <summary>Gets the default value as an untyped object.</summary>
    public abstract object? UntypedDefault { get; }

    /// <summary>Returns <see langword="true"/> if the current value equals the default.</summary>
    public virtual bool IsDefault => EqualityComparer<object>.Default.Equals(UntypedValue, UntypedDefault);

    /// <inheritdoc/>
    public event PropertyChangedEventHandler? PropertyChanged;
    
    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    
    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}