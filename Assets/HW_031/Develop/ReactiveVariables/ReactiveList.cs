using System;
using System.Collections.Generic;

public class ReactiveList<T>
{
    public event Action<T> Added;
    public event Action<T> Removed;

    private readonly List<T> _items = new();

    public IReadOnlyList<T> Items => _items;

    public void Add(T item)
    {
        _items.Add(item);

        Added?.Invoke(item);
    }

    public void Remove(T item)
    {
        if (_items.Remove(item))
            Removed?.Invoke(item);
    }
}