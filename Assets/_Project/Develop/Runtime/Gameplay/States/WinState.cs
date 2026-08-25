using Assets._Project.Develop.Runtime.Configs.Gameplay.Levels;
using Assets._Project.Develop.Runtime.Gameplay.Features.InputFeature;
using Assets._Project.Develop.Runtime.Gameplay.Features.PlayerInteraction;
using Assets._Project.Develop.Runtime.Meta.Features.Statistics;
using Assets._Project.Develop.Runtime.Meta.Features.Wallet;
using Assets._Project.Develop.Runtime.Utilities.CoroutinesManagment;
using Assets._Project.Develop.Runtime.Utilities.DataManagment.DataProviders;
using Assets._Project.Develop.Runtime.Utilities.SceneManagment;
using Assets._Project.Develop.Runtime.Utilities.StateMachineCore;
using System.Collections;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.States
{
    public class WinState : EndGameState, IUpdatableState
    {
        private readonly GameStatsService _statisticsService;
        private readonly WalletService _walletService;
        private readonly LevelConfig _levelConfig;
        private readonly PlayerDataProvider _playerDataProvider;
        private readonly SceneSwitcherService _sceneSwitcherService;
        private readonly ICoroutinesPerformer _coroutinesPerformer;

        public WinState(
            IInputService inputService,
            PlayerService playerService,
            GameStatsService statisticsService,
            WalletService walletService,
            LevelConfig levelConfig,
            PlayerDataProvider playerDataProvider,
            SceneSwitcherService sceneSwitcherService,
            ICoroutinesPerformer coroutinesPerformer) : base(inputService, playerService)
        {
            _statisticsService = statisticsService;
            _walletService = walletService;
            _levelConfig = levelConfig;
            _playerDataProvider = playerDataProvider;
            _sceneSwitcherService = sceneSwitcherService;
            _coroutinesPerformer = coroutinesPerformer;
        }

        public override void Enter()
        {
            base.Enter();

            Debug.Log($"ПОБЕДА! +{_levelConfig.RewardGold} золота");

            _walletService.Add(CurrencyTypes.Gold, _levelConfig.RewardGold);
            _statisticsService.RegisterWin();

            _coroutinesPerformer.StartPerform(ReturnToMenu());
        }

        public void Update(float deltaTime)
        {
        }

        private IEnumerator ReturnToMenu()
        {
            yield return _playerDataProvider.SaveAsync();
            yield return new WaitForSeconds(1.5f);
            yield return _sceneSwitcherService.ProcessSwitchTo(Scenes.MainMenu);
        }
    }
}
