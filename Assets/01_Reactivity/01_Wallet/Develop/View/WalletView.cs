using System.Collections.Generic;
using UnityEngine;

namespace Wallet
{
    public class WalletView : MonoBehaviour 
    {
        [SerializeField] private List<CurrencyView> _currencyViews;
        private ReactiveDictionary<ItemType, int> _reactiveVar;


        private Wallet _wallet;

        private Dictionary<ItemType, int> _values = new();

        public void Initialize(Wallet wallet)
        {
            _wallet = wallet;
            _reactiveVar = wallet.Storage;

            _reactiveVar.Changed += OnValueChanged;

            foreach (CurrencyView currencyView in _currencyViews)
                currencyView.Hide();
        }

        private void OnDestroy()
        {
            if (_wallet != null)
                _reactiveVar.Changed -= OnValueChanged;
        }

        private void OnValueChanged(ItemType itemType, int value)
        {
            _values[itemType] = value;

            Draw();
        }

        private void Draw()
        {
            int index = 0;

            foreach (KeyValuePair<ItemType, int> pair in _values)
            {
                if (pair.Value <= 0)
                    continue;

                if (index >= _currencyViews.Count)
                    break;

                _currencyViews[index].Render(pair.Key, pair.Value);

                index++;
            }

            for (int i = index; i < _currencyViews.Count; i++)
            {
                _currencyViews[i].Hide();
            }
        }
    }
}