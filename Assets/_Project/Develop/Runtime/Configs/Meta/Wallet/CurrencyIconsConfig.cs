using Assets._Project.Develop.Runtime.Meta.Features.Wallet;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Configs.Meta.Wallet
{
    [CreateAssetMenu(menuName = "Configs/Meta/Wallet/NewCurrencyIconsConfig", fileName = "CurrencyIconsConfig")]
    public class CurrencyIconsConfig : ScriptableObject
    {
        [SerializeField] private List<CurrencyConfig> _configs = new();

        public Sprite GetSpriteFor(CurrencyTypes currencyType)
        {
            if (_configs == null)
                return null;

            for (int i = 0; i < _configs.Count; i++)
            {
                if (_configs[i].Type == currencyType)
                    return _configs[i].Sprite;
            }

            return null;
        }

        [Serializable]
        public class CurrencyConfig
        {
            public CurrencyTypes Type;
            public Sprite Sprite;
        }
    }
}
