using System;
using UnityEngine;

namespace Wallet
{
    public class WalletTestingInput : MonoBehaviour
    {
        public event Action <ItemType, int> AddItem;
        public event Action <ItemType, int> SpendItem;

        [SerializeField] private int _addCountCoin = 1;
        [SerializeField] private int _addCountEnergy = 2;
        [SerializeField] private int _addCountGem = 3;

        [Header("")]
        [SerializeField] private int _spendCountCoin = 1;
        [SerializeField] private int _spendCountEnergy = 2;
        [SerializeField] private int _spendCountGem = 3;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
                AddItem?.Invoke(ItemType.Coin, _addCountCoin);

            if (Input.GetKeyDown(KeyCode.W))
                AddItem?.Invoke(ItemType.Energy, _addCountEnergy);

            if (Input.GetKeyDown(KeyCode.E))
                AddItem?.Invoke(ItemType.Gem, _addCountGem);

            if (Input.GetKeyDown(KeyCode.A))
                SpendItem?.Invoke(ItemType.Coin, _spendCountCoin);

            if (Input.GetKeyDown(KeyCode.S))
                SpendItem?.Invoke(ItemType.Energy, _spendCountEnergy);

            if (Input.GetKeyDown(KeyCode.D))
                SpendItem?.Invoke(ItemType.Gem, _spendCountGem);
        }
    }
}