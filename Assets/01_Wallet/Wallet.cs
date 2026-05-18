using System;
using System.Collections.Generic;
using UnityEngine;

namespace Wallet
{
    public class Wallet
    {
        public event Action<ItemType, int> CoinValueChanged;

        private Dictionary<ItemType, int> _storage = new Dictionary<ItemType, int>();

        public void AddCurrency(ItemType itemType, int count)
        {
            UpdateWallet(itemType, count);

            CoinValueChanged?.Invoke(itemType, _storage.GetValueOrDefault(itemType));
        }

        public void TrySpendCurrency(ItemType itemType, int count)
        {
            UpdateWallet(itemType, -Mathf.Abs(count));

            CoinValueChanged?.Invoke(itemType, _storage.GetValueOrDefault(itemType));
        }

        private void UpdateWallet(ItemType itemType, int count)
        {
            if (_storage.ContainsKey(itemType))
                _storage[itemType] += count;
            else if (count > 0)
                _storage[itemType] = count;

            if (_storage.ContainsKey(itemType) && _storage[itemType] <= 0)
                _storage.Remove(itemType);
        }
    }
}