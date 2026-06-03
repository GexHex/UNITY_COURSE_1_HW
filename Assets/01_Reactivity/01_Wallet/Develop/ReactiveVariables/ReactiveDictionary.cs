using System;
using System.Collections.Generic;

namespace Wallet
{
    public class ReactiveDictionary<TKey, TValue> : IReadOnlyReactiveDictionary<TKey, TValue>
    {
        public event Action<TKey, TValue> Changed;

        private readonly Dictionary<TKey, TValue> _storage = new();

        public IReadOnlyDictionary<TKey, TValue> Values => _storage;

        public bool ContainsKey(TKey key)
        {
            return _storage.ContainsKey(key);
        }

        public TValue GetValueOrDefault(TKey key)
        {
            return _storage.GetValueOrDefault(key);
        }

        public void Set(TKey key, TValue value)
        {
            _storage[key] = value;

            Changed?.Invoke(key, value);
        }

        public void Remove(TKey key)
        {
            if (_storage.Remove(key))
            {
                Changed?.Invoke(key, default);
            }
        }
    }
}