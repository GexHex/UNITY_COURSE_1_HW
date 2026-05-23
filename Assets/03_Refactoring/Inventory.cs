using System.Collections.Generic;

public class Inventory
{
    private readonly List<Item> _items;
    private readonly int _maxCount;

    public IReadOnlyList<Item> Items => _items;

    public int CurrentCount => _items.Count;

    public Inventory(int maxCount)
    {
        _maxCount = maxCount;
        _items = new List<Item>();
    }

    public bool TryAdd(Item item)
    {
        if (item == null)
            return false;

        if (CurrentCount >= _maxCount)
            return false;

        _items.Add(item);

        return true;
    }

    public List<Item> GetItemsBy(string name, int count)
    {
        List<Item> receivedItems = new();

        if (count <= 0)
            return receivedItems;

        for (int i = _items.Count - 1; i >= 0; i--)
        {
            if (_items[i].Name != name)
                continue;

            receivedItems.Add(_items[i]);
            _items.RemoveAt(i);

            if (receivedItems.Count >= count)
                break;
        }

        return receivedItems;
    }
}