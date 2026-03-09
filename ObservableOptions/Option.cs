namespace ObservableOptions;

/// <summary>
/// A typed option that holds a single value and fires <see cref="ValueChanged"/> when it changes.
/// </summary>
/// <typeparam name="T">The value type of this option.</typeparam>
public class Option<T> : OptionBase
{
    /// <summary>The default value this option resets to.</summary>
    public required T Default { get; init; }

    /// <summary>Gets or sets the current value. Fires <see cref="ValueChanged"/> on change.</summary>
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

    /// <summary>Raised when <see cref="Value"/> changes.</summary>
    public event EventHandler<OptionValueChangedEventArgs<T>>? ValueChanged;

    /// <summary>Invokes <see cref="ValueChanged"/> with the old and new value.</summary>
    protected virtual void OnValueChanged(T oldValue, T newValue)
    {
        ValueChanged?.Invoke(this, new  OptionValueChangedEventArgs<T>(oldValue, newValue));
    }

    /// <inheritdoc/>
    public override object? UntypedValue 
    { 
        get => Value;
        set => Value = (T)value!;
    }

    /// <summary>Resets <see cref="Value"/> to <see cref="Default"/>.</summary>
    public void ResetToDefault()
    {
        UntypedValue = Default;
    }
    
    /// <inheritdoc/>
    public override object? UntypedDefault => Default;
}

/// <summary>
/// Event arguments for <see cref="Option{T}.ValueChanged"/>.
/// </summary>
/// <typeparam name="T">The value type.</typeparam>
public class OptionValueChangedEventArgs<T>(T oldValue, T newValue) : EventArgs
{
    /// <summary>The value before the change.</summary>
    public T OldValue { get; init; } = oldValue;

    /// <summary>The value after the change.</summary>
    public T NewValue { get; init; } = newValue;
}