using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace ObservableOptions;

public class CollectionOption<T> : OptionBase
{
    private readonly T[] _defaultItems;

    private ObservableCollection<T> _collection;

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
    public override object? UntypedDefault => _defaultItems;

    public override bool IsDefault => Collection.SequenceEqual(_defaultItems);

    public void Add(T value)
    {
        Collection.Add(value);
    }

    public void ResetToDefault()
    {
        UntypedValue = _defaultItems;
    }
    
    public void Clear()
    {
        Collection.Clear();
    }

    public bool Contains(T value)
    {
        return Collection.Contains(value);
    }
    
    public void Remove(T value)
    {
        Collection.Remove(value);
    }
}