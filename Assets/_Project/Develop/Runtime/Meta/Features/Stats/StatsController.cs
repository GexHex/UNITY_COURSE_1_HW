using Assets._Project.Develop.Runtime.Configs.Meta.Level;
using Assets._Project.Develop.Runtime.Meta.Features.ScoreCounter;
using Assets._Project.Develop.Runtime.Meta.Features.Wallet;
using Assets._Project.Develop.Runtime.Utilities.ConfigsManagment;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Meta.Features.Stats
{
    public class StatsController
    {
        private readonly StatsService _statsService;
        private readonly WalletService _walletService;
        private readonly ConfigsProviderService _configsProviderService;
        private readonly StatsInfo _statsInfo;

        private readonly int _goldCountToReset;

        public StatsController(
            StatsService statsService,
            WalletService walletService,
            ConfigsProviderService configsProviderService,
            StatsInfo view)
        {
            _statsService = statsService;
            _walletService = walletService;
            _configsProviderService = configsProviderService;
            _statsInfo = view;

            _goldCountToReset = _configsProviderService.GetConfig<GameBalanceConfig>().GoldToReset;
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                ShowStats();
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                ResetStatsForGold();
            }
        }

        public void ResetStatsForGold()
        {
            if (_walletService.Enough(CurrencyTypes.Gold, _goldCountToReset) == false)
            {
                _statsInfo.ShowError();
                return;
            }

            _walletService.Spend(CurrencyTypes.Gold, _goldCountToReset);
            _statsService.ResetStats();

            ShowStats();
        }

        public void ShowStats()
        {
            int currentGold = _walletService.GetCurrency(CurrencyTypes.Gold).Value;
            _statsInfo.ShowStats(_statsService.Wins.Value, _statsService.Losses.Value, currentGold);
        }
    }
}