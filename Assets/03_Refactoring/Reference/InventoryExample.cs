using System.Collections.Generic;
using System.Linq;

namespace example
{
    public class Inventory
    {
        public List<Item> _items = new();

        public int CurrentSize => _items.Sum(item => item.Count);

        public int MaxSize;

        public Inventory(List<Item> items, int maxSize)
        {
            _items = items;
            MaxSize = maxSize;
        }

        public void Add(Item item)
        {
            if (CurrentSize + item.Count > MaxSize)
                return;

            _items.Add(item);
        }

        public List<Item> GetItemsBy(string name, int count)
        {
            _items = new List<Item>();

            for (int i = 0; i < count; i++)
            {
                Item item = _items.First(item => item.Name == name);
                _items.Remove(item);
            }

            return _items;
        }
    }

    public class Item
    {
        public string Name;
        public int Count;
    }
}