using UnityEngine;

namespace Wallet
{
    public class Wallet
    {
        private ReactiveDictionary<ItemType, int> _storage = new();

        public ReactiveDictionary<ItemType, int> Storage => _storage;

        public void AddCurrency(ItemType itemType, int count)
        {
            UpdateWallet(itemType, count);
        }

        public void TrySpendCurrency(ItemType itemType, int count)
        {
            UpdateWallet(itemType, -Mathf.Abs(count));
        }

        private void UpdateWallet(ItemType itemType, int count)
        {
            if (_storage.ContainsKey(itemType))
                _storage[itemType] += count;
            else if (count > 0)
                _storage.Add(itemType, count);

            if (_storage.ContainsKey(itemType) && _storage[itemType] <= 0)
                _storage.Remove(itemType);
        }
    }
}