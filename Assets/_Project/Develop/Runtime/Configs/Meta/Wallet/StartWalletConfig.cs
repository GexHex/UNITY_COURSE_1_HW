using Assets._Project.Develop.Runtime.Meta.Features.Wallet;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Configs.Meta.Wallet
{
    [CreateAssetMenu(menuName = "Configs/Meta/Wallet/NewStartWalletConfig", fileName = "StartWalletConfig")]
    public class StartWalletConfig : ScriptableObject
    {
        [SerializeField] private List<CurrencyConfig> _values = new();

        public int GetValueFor(CurrencyTypes currencyType)
        {
            if (_values != null)
            {
                for (int i = 0; i < _values.Count; i++)
                {
                    if (_values[i].Type == currencyType)
                        return _values[i].Value;
                }
            }

            return currencyType == CurrencyTypes.Gold ? 40 : 0;
        }

        [Serializable]
        public class CurrencyConfig
        {
            public CurrencyTypes Type;
            public int Value;
        }
    }
}
