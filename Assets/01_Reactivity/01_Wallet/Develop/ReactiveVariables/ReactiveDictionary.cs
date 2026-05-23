using System;
using System.Collections.Generic;

namespace Wallet
{
    public class ReactiveDictionary<ItemType, TValue>
    {
        public event Action<ItemType, int> Changed;

        private readonly Dictionary<ItemType, int> _storage = new();

        public bool ContainsKey(ItemType key)
        {
            return _storage.ContainsKey(key);
        }

        public int GetValueOrDefault(ItemType key)
        {
            return _storage.GetValueOrDefault(key);
        }

        public void Add(ItemType key, int value)
        {
            _storage[key] = value;

            Changed?.Invoke(key, value);
        }

        public void Remove(ItemType key)
        {
            _storage.Remove(key);

            Changed?.Invoke(key, default);
        }

        public int this[ItemType key]
        {
            get => _storage[key];

            set
            {
                _storage[key] = value;

                Changed?.Invoke(key, value);
            }
        }
    }
}