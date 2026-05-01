using System;
using System.Collections.Generic;

namespace Wallet
{
    public class Wallet
    {
        public event Action<ItemType, int, string> CoinValueChanged;
        Dictionary<ItemType, int> _storage = new Dictionary<ItemType, int>();

        public void ChangeCoin(ItemType type, int delta)
        {
            if (_storage.ContainsKey(type))
                _storage[type] += delta;
            else if (delta > 0)
                _storage[type] = delta;

            if (_storage.ContainsKey(type) && _storage[type] <= 0)
                _storage.Remove(type);

            CoinValueChanged?.Invoke(type, _storage.GetValueOrDefault(type), null);
        }
    }
}