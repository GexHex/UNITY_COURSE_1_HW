using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Wallet
{
    public class WalletPrintInfo : MonoBehaviour
    {
        [SerializeField] private List<TMP_Text> _texts;
        [SerializeField] private List<Image> _icons;
        [SerializeField] private Sprite _coinIcon;
        [SerializeField] private Sprite _gemIcon;
        [SerializeField] private Sprite _energyIcon;

        private Wallet _wallet;
        private List<ItemType> _activeItems = new();
        private Dictionary<ItemType, int> _values = new();
        private Dictionary<ItemType, Sprite> _iconImage;

        public void Initialize(Wallet wallet)
        {
            _wallet = wallet;
     
            _wallet.CoinValueChanged += OnCoinValueChanged;

            _iconImage = new Dictionary<ItemType, Sprite>
            { { ItemType.Coin, _coinIcon },
            { ItemType.Gem, _gemIcon },
            { ItemType.Energy, _energyIcon } };

            foreach (Image icon in _icons)
                icon.enabled = false;
        }

        private void OnDestroy()
        {
            _wallet.CoinValueChanged -= OnCoinValueChanged;
        }

        private void OnCoinValueChanged(ItemType coinType, int count, string massage)
        {
            Debug.Log($"Coin value changed {coinType} : {count} : {massage}");

            SetTexts(coinType, count);
            SetIcons(coinType, count);
        }

        private void SetTexts(ItemType coinType, int count)
        {
            _values[coinType] = count;

            if (count > 0 && !_activeItems.Contains(coinType))
                _activeItems.Add(coinType);

            if (count <= 0 && _activeItems.Contains(coinType))
                _activeItems.Remove(coinType);

            for (int i = 0; i < _texts.Count; i++)
            {
                if (i < _activeItems.Count)
                {
                    ItemType type = _activeItems[i];
                    _texts[i].text = $"{type} : {_values[type]}";
                }
                else
                {
                    _texts[i].text = "";
                }
            }
        }

        private void SetIcons(ItemType coinType, int count)
        {
            for (int i = 0; i < _icons.Count; i++)
            {
                if (i < _activeItems.Count)
                {
                    ItemType type = _activeItems[i];
                    _icons[i].sprite = _iconImage[type];
                    _icons[i].enabled = true;
                }
                else
                {
                    _icons[i].enabled = false;
                }
            }
        }
    }
}