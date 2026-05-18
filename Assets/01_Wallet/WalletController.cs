using UnityEngine;

namespace Wallet
{
    public class WalletController : MonoBehaviour
    {
        private WalletTestingInput _input;
        private Wallet _wallet;

        public void Initialize(WalletTestingInput inputUser, Wallet wallet)
        {
            _input = inputUser;
            _wallet = wallet;

            _input.AddItem += OnAddCoin;
            _input.SpendItem += OnDeleteCoin;
        }

        private void OnDestroy()
        {
            _input.AddItem -= OnAddCoin;
            _input.SpendItem -= OnDeleteCoin;
        }

        private void OnAddCoin(ItemType itemType, int addValueCoin)
        {
            if (addValueCoin >= 0)
                _wallet.AddCurrency(itemType, addValueCoin);
        }

        private void OnDeleteCoin(ItemType itemType, int spendValueCoin)
        {
            if (spendValueCoin >= 0)
                _wallet.TrySpendCurrency(itemType, spendValueCoin);
        }
    }
}