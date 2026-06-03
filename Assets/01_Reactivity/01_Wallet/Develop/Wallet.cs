using UnityEngine;

namespace Wallet
{
    public class Wallet
    {
        private ReactiveDictionary<ItemType, int> _storage = new();

        public IReadOnlyReactiveDictionary<ItemType, int> Storage => _storage;

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
            int currentValue = _storage.GetValueOrDefault(itemType);

            int newValue = currentValue + count;

            if (newValue > 0)
                _storage.Set(itemType, newValue);
            else
                _storage.Remove(itemType);
        }
    }
}