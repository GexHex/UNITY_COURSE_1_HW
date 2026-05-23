using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Wallet
{
    public class CurrencyView : MonoBehaviour
    {
        [Header("")]
        [SerializeField] private TMP_Text _text;
        [SerializeField] private Image _icon;

        [Header("")]
        [SerializeField] private Sprite _coinIcon;
        [SerializeField] private Sprite _gemIcon;
        [SerializeField] private Sprite _energyIcon;

        private Dictionary<ItemType, Sprite> _icons;

        private void Awake()
        {
            _icons = new Dictionary<ItemType, Sprite>
        {
            { ItemType.Coin, _coinIcon },
            { ItemType.Gem, _gemIcon },
            { ItemType.Energy, _energyIcon }
        };
        }

        public void Render(ItemType itemType, int value)
        {
            gameObject.SetActive(true);

            _text.text = $"{itemType} : {value}";
            _icon.sprite = _icons[itemType];
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}