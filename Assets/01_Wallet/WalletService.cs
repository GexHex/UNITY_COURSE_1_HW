using UnityEngine;

namespace Wallet
{
    public class WalletService : MonoBehaviour
    {
        [SerializeField] private InputUser _inputUser;
        [SerializeField] private WalletController _walletController;
        [SerializeField] private WalletPrintInfo _walletPrintInfo;
        private Wallet _wallet;

        private void Awake()
        {
            _wallet = new Wallet();
            _walletPrintInfo.Initialize(_wallet);
            _walletController.Initialize(_inputUser, _wallet);
        }
    }
}