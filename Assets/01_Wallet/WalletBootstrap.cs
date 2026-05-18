using UnityEngine;

namespace Wallet
{
    public class WalletBootstrap : MonoBehaviour
    {
        [SerializeField] private WalletTestingInput _inputUser;
        [SerializeField] private WalletController _walletController;
        [SerializeField] private WalletView _walletView;

        private Wallet _wallet;

        private void Awake()
        {
            _wallet = new Wallet();
            _walletView.Initialize(_wallet);
            _walletController.Initialize(_inputUser, _wallet);
        }
    }
}