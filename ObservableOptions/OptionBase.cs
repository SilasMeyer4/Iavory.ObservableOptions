using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ObservableOptions;

public abstract class OptionBase : INotifyPropertyChanged
{
    public string Key { get; internal set; } = string.Empty;
    public string Text { get; init; } = string.Empty;
    public string? Description { get; init; } = string.Empty;
    
    public abstract object? UntypedValue { get; set; }
    public abstract object? UntypedDefault { get; }
    
    public virtual bool IsDefault => EqualityComparer<object>.Default.Equals(UntypedValue, UntypedDefault);
    
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