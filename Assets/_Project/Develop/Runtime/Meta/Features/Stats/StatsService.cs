using Assets._Project.Develop.Runtime.Configs.Meta.Level;
using Assets._Project.Develop.Runtime.Meta.Features.Wallet;
using Assets._Project.Develop.Runtime.Utilities.ConfigsManagment;
using Assets._Project.Develop.Runtime.Utilities.CoroutinesManagment;
using Assets._Project.Develop.Runtime.Utilities.DataManagment;
using Assets._Project.Develop.Runtime.Utilities.DataManagment.DataProviders;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using System;
using System.Collections;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Meta.Features.ScoreCounter
{
    public class StatsService : IDataReader<PlayerData>, IDataWriter<PlayerData>
    {
        private readonly ReactiveVariable<int> _wins = new(0);
        private readonly ReactiveVariable<int> _losses = new(0);

        private WalletService _walletService;
        private ConfigsProviderService _configsProviderService;
        private PlayerDataProvider _playerDataProvider;
        private ICoroutinesPerformer _coroutinesPerformer;
        private int _goldPerWin;
        private int _goldPerLose;
        private int _resetGoldCount;

        public StatsService(
            PlayerDataProvider playerDataProvider,
            WalletService walletService,
            ConfigsProviderService configPrividerService,
            ICoroutinesPerformer coroutinesPerformer)
        {
            _walletService = walletService;
            _configsProviderService = configPrividerService;
            _coroutinesPerformer = coroutinesPerformer;
            _playerDataProvider= playerDataProvider;

            playerDataProvider.RegisterReader(this);
            playerDataProvider.RegisterWriter(this);

            _goldPerWin = _configsProviderService.GetConfig<GameBalanceConfig>().GoldToWin;
            _goldPerLose = _configsProviderService.GetConfig<GameBalanceConfig>().GoldToLose;
            _resetGoldCount = _configsProviderService.GetConfig<GameBalanceConfig>().GoldToReset;
        }       

        public IReadOnlyVariable<int> Wins => _wins;
        public IReadOnlyVariable<int> Losses => _losses;

        public void Run()
        {
            _coroutinesPerformer.StartPerform(LoadStats());

            ShowStats(CurrencyTypes.Gold);
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {               
                ShowStats(CurrencyTypes.Gold);
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                ResetStatsForGold();  

                ShowStats(CurrencyTypes.Gold);  
            }
        }

        public void AddWin()
        {
            _wins.Value += 1;
        }

        public void AddLoss()
        {
            _losses.Value += 1;
        }

        public void ResetStats()
        {
            _wins.Value = 0;
            _losses.Value = 0;
        }

        public void ReadFrom(PlayerData data)
        {
            _wins.Value = data.Wins;
            _losses.Value = data.Loses;
        }

        public void WriteTo(PlayerData data)
        {
            data.Wins = _wins.Value;
            data.Loses = _losses.Value;
        }

        public void AddCurrency(CurrencyTypes cyrencyTypes)
        {
            _walletService.Add(cyrencyTypes, _goldPerWin);
        }

        public void SpendCurrency(CurrencyTypes cyrencyTypes)
        {
            if (_walletService.Enough(cyrencyTypes, _goldPerLose))
                _walletService.Spend(cyrencyTypes, _goldPerLose);
            else
                Debug.Log("Нет золота для удаления!");
        }

        public void ResetStatsForGold()
        {
            if (_walletService.Enough(CurrencyTypes.Gold, _resetGoldCount) == false)
                new ArgumentOutOfRangeException("Денег для сброса кошелька нет!");

            _walletService.Spend(CurrencyTypes.Gold, _resetGoldCount);

            _wins.Value = 0;
            _losses.Value = 0;
        }

        public void ShowStats(CurrencyTypes cyrencyTypes)
        {
            Debug.Log($"-----------------Stats-----------------");
            Debug.Log($"Золото: {_walletService.GetCurrency(CurrencyTypes.Gold).Value}");
            Debug.Log($"Побед: {_wins.Value}");
            Debug.Log($"Поражений: {_losses.Value}");
            Debug.Log($"---------------------------------------");
        }

        private IEnumerator LoadStats()
        {
            yield return _coroutinesPerformer.StartPerform(_playerDataProvider.Load());
        }
    }
}