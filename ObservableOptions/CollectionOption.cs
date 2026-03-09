using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace ObservableOptions;

/// <summary>
/// An option that manages an <see cref="ObservableCollection{T}"/> of items and tracks a selected item.
/// </summary>
/// <typeparam name="T">The element type of the collection.</typeparam>
public class CollectionOption<T> : OptionBase
{
    private readonly T[] _defaultItems;

    private ObservableCollection<T> _collection;

    /// <summary>Gets or sets the currently selected item.</summary>
    public T? SelectedItem
    {
        get => field;
        set
        {
            if (SetField(ref field, value))
            {
                
            }
        }
    }

    /// <summary>Initializes a new instance with optional default items.</summary>
    /// <param name="defaultItems">Items used as the default state. Defaults to an empty collection.</param>
    public CollectionOption(IEnumerable<T>? defaultItems = null)
    {
        _defaultItems = (defaultItems ?? Enumerable.Empty<T>()).ToArray();
        _collection = new ObservableCollection<T>(_defaultItems);
        _collection.CollectionChanged += OnCollectionChanged;
    }

    private void OnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        OnPropertyChanged(nameof(Collection));
        OnPropertyChanged(nameof(UntypedValue));
    }

    /// <summary>Gets or sets the underlying <see cref="ObservableCollection{T}"/>. Raises <see cref="OptionBase.PropertyChanged"/> on replacement.</summary>
    public ObservableCollection<T> Collection
    {
        get => _collection;
        set
        {
            if (_collection != value)
            {
                _collection.CollectionChanged -= OnCollectionChanged;
                _collection = value ?? throw new ArgumentNullException(nameof(value));
                _collection.CollectionChanged += OnCollectionChanged;
                OnPropertyChanged();
                OnPropertyChanged(nameof(UntypedValue));
            }
        }
    }

    /// <inheritdoc/>
    public override object? UntypedValue
    {
        get => Collection.ToArray();
        set
        {
            var items = value switch
            {
                null => Array.Empty<T>(),
                T[] arr => arr,
                IEnumerable<T> enumerable => enumerable.ToArray(),
                _ => throw new ArgumentException(
                    $"Expected {typeof(IEnumerable<T>).Name}<{typeof(T).Name}> or {typeof(T).Name}[], got {value.GetType().Name}.")
            };
            
            Collection.CollectionChanged -= OnCollectionChanged;

            try
            {
                Collection.Clear();
                foreach (var item in items) Collection.Add(item);
            }
            finally
            {
                Collection.CollectionChanged += OnCollectionChanged;
            }
            OnPropertyChanged(nameof(UntypedValue));
            OnPropertyChanged(nameof(Collection));
        }
    }
    /// <inheritdoc/>
    public override object? UntypedDefault => _defaultItems;

    /// <summary>Returns <see langword="true"/> if the collection matches the default items in order.</summary>
    public override bool IsDefault => Collection.SequenceEqual(_defaultItems);

    /// <summary>Adds <paramref name="value"/> to the collection.</summary>
    public void Add(T value)
    {
        Collection.Add(value);
    }

    /// <summary>Resets the collection to the default items.</summary>
    public void ResetToDefault()
    {
        UntypedValue = _defaultItems;
    }

    /// <summary>Removes all items from the collection.</summary>
    public void Clear()
    {
        Collection.Clear();
    }

    /// <summary>Returns <see langword="true"/> if the collection contains <paramref name="value"/>.</summary>
    public bool Contains(T value)
    {
        return Collection.Contains(value);
    }

    /// <summary>Removes the first occurrence of <paramref name="value"/> from the collection.</summary>
    public void Remove(T value)
    {
        Collection.Remove(value);
    }
}