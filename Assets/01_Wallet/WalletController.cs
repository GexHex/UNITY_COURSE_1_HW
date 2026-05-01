using UnityEngine;

namespace Wallet
{
    public class WalletController : MonoBehaviour
    {
        private InputUser _input;
        private Wallet _wallet;

        private int _addValueCoin = 1;
        private int _addValueGem = 2;
        private int _addValueEnergy = 3;

        private int _deleteValueCoin = -1;
        private int _deleteValueGem = -2;
        private int _deleteValueEnergy = -3;

        public void Initialize(InputUser inputUser,Wallet wallet)
        {
            _input = inputUser;
            _wallet = wallet;
       
            _input.AddItem01 += OnAddCoin;
            _input.AddItem02 += OnAddGem;
            _input.AddItem03 += OnAddEnergy;

            _input.DeleteItem01 += OnDeleteCoin;
            _input.DeleteItem02 += OnDeleteGem;
            _input.DeleteItem03 += OnDeleteEnergy;
        }

        private void OnDestroy()
        {
            _input.AddItem01 -= OnAddCoin;
            _input.AddItem02 -= OnAddGem;
            _input.AddItem03 -= OnAddEnergy;

            _input.DeleteItem01 -= OnDeleteCoin;
            _input.DeleteItem02 -= OnDeleteGem;
            _input.DeleteItem03 -= OnDeleteEnergy;
        }

        private void OnAddCoin() => _wallet.ChangeCoin(ItemType.Coin, _addValueCoin);
        private void OnAddGem() => _wallet.ChangeCoin(ItemType.Gem, _addValueGem);
        private void OnAddEnergy() => _wallet.ChangeCoin(ItemType.Energy, _addValueEnergy);
        private void OnDeleteCoin() => _wallet.ChangeCoin(ItemType.Coin, _deleteValueCoin);
        private void OnDeleteGem() => _wallet.ChangeCoin(ItemType.Gem, _deleteValueGem);
        private void OnDeleteEnergy() => _wallet.ChangeCoin(ItemType.Energy, _deleteValueEnergy);
    }
}