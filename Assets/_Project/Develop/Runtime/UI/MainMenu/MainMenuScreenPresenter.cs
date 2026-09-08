using Assets._Project.Develop.Runtime.Configs.Gameplay.Levels;
using Assets._Project.Develop.Runtime.Gameplay.Infrastructure;
using Assets._Project.Develop.Runtime.Meta.Features.Statistics;
using Assets._Project.Develop.Runtime.UI.Core;
using Assets._Project.Develop.Runtime.UI.Statistics;
using Assets._Project.Develop.Runtime.UI.Wallet;
using Assets._Project.Develop.Runtime.Utilities.ConfigsManagment;
using Assets._Project.Develop.Runtime.Utilities.CoroutinesManagment;
using Assets._Project.Develop.Runtime.Utilities.SceneManagment;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.UI.MainMenu
{
    public class MainMenuScreenPresenter : IPresenter
    {
        private readonly MainMenuScreenView _screen;
        private readonly ProjectPresentersFactory _projectPresentersFactory;
        private readonly GameStatsService _gameStatsService;
        private readonly ConfigsProviderService _configsProviderService;
        private readonly SceneSwitcherService _sceneSwitcherService;
        private readonly ICoroutinesPerformer _coroutinesPerformer;

        private readonly List<IPresenter> _childPresenters = new();

        public MainMenuScreenPresenter(
            MainMenuScreenView screen,
            ProjectPresentersFactory projectPresentersFactory,
            GameStatsService statisticsService,
            ConfigsProviderService configsProviderService,
            SceneSwitcherService sceneSwitcherService,
            ICoroutinesPerformer coroutinesPerformer)
        {
            _screen = screen;
            _projectPresentersFactory = projectPresentersFactory;
            _gameStatsService = statisticsService;
            _configsProviderService = configsProviderService;
            _sceneSwitcherService = sceneSwitcherService;
            _coroutinesPerformer = coroutinesPerformer;
        }

        public void Initialize()
        {
            _screen.PlayButtonClicked += OnPlayButtonClicked;

            CreateWallet();
            CreateStats();

            foreach (IPresenter presenter in _childPresenters)
                presenter.Initialize();
        }

        public void Dispose()
        {
            _screen.PlayButtonClicked -= OnPlayButtonClicked;

            foreach (IPresenter presenter in _childPresenters)
                presenter.Dispose();

            _childPresenters.Clear();
        }

        private void CreateWallet()
        {
            WalletPresenter walletPresenter = _projectPresentersFactory.CreateWalletPresenter(_screen.WalletView);
            
            _childPresenters.Add(walletPresenter);
        }

        private void CreateStats()
        {
            StatsPresenter winsPresenter = _projectPresentersFactory.CreateStatisticPresenter(
                _screen.Wins,
                _gameStatsService.Wins,
                "Wins: ");

            StatsPresenter lossesPresenter = _projectPresentersFactory.CreateStatisticPresenter(
                _screen.Defeats,
                _gameStatsService.Losses,
                "Loses: ");

            _childPresenters.Add(winsPresenter);
            _childPresenters.Add(lossesPresenter);
        }

        private void OnPlayButtonClicked()
        {
            LevelsListConfig levelsListConfig = _configsProviderService.GetConfig<LevelsListConfig>();
            int levelNumber = levelsListConfig.GetRandomLevelNumber();

            Debug.Log($"Запуск случайного уровня {levelNumber}");

            _coroutinesPerformer.StartPerform(
                _sceneSwitcherService.ProcessSwitchTo(Scenes.Gameplay, new GameplayInputArgs(levelNumber)));
        }
    }
}
