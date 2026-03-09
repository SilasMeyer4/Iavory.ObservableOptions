namespace ObservableOptions;

public class Option<T> : OptionBase
{
    public required T Default { get; init; }

    public T Value
    {
        get;
        set
        {
            var oldValue = value;
            if (SetField(ref field, value))
            {
                OnValueChanged(oldValue, field);
            }
        }
    } = default!;

    public event EventHandler<OptionValueChangedEventArgs<T>>? ValueChanged;

    protected virtual void OnValueChanged(T oldValue, T newValue)
    {
        ValueChanged?.Invoke(this, new  OptionValueChangedEventArgs<T>(oldValue, newValue));
    }

    public override object? UntypedValue 
    { 
        get => Value;
        set => Value = (T)value!;
    }

    public void ResetToDefault()
    {
        UntypedValue = Default;
    }
    
    public override object? UntypedDefault => Default;
}

public class OptionValueChangedEventArgs<T>(T oldValue, T newValue) : EventArgs
{
    public T OldValue { get; init; } = oldValue;
    public T NewValue { get; init; } = newValue;
}