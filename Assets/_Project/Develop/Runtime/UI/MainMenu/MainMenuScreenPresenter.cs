using Assets._Project.Develop.Runtime.Meta.Features.Stats;
using Assets._Project.Develop.Runtime.UI.Core;
using Assets._Project.Develop.Runtime.UI.Wallet;
using Assets._Project.Develop.Runtime.Utilities.UI;
using System.Collections.Generic;

namespace Assets._Project.Develop.Runtime.UI.MainMenu
{
    public class MainMenuScreenPresenter : IPresenter
    {
        private readonly MainMenuScreenView _screen;
        private readonly ProjectPresentersFactory _projectPresentersFactory;
        private readonly StatsController _statsController;
        private readonly List<IPresenter> _childPresenters = new();
        private readonly GameModeChooseService _gameModeChooseService;

        public MainMenuScreenPresenter(
            MainMenuScreenView screen,
            ProjectPresentersFactory projectPresentersFactory,
            MainMenuPopupService popupService,
            StatsController statsController,
            GameModeChooseService gameModeChooseService)
        {
            _screen = screen;
            _projectPresentersFactory = projectPresentersFactory;
            _statsController = statsController;
            _gameModeChooseService = gameModeChooseService;
        }

        public void Initialize()
        {
            _screen.ResetStatsButtonClicked += OnResetStatsButtonClicked;
            _screen.DigitsButtonClicked += OnDigitsButtonClicked;
            _screen.LeterstButtonClicked += OnLeterstButtonClicked;

            CreateWallet();
            CreateStats();

            foreach (IPresenter presenter in _childPresenters)
                presenter.Initialize();
        }

        public void Dispose()
        {
            _screen.ResetStatsButtonClicked -= OnResetStatsButtonClicked;
            _screen.DigitsButtonClicked -= OnDigitsButtonClicked;
            _screen.LeterstButtonClicked -= OnLeterstButtonClicked;


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
            var targetView = _screen.StatsView != null ? _screen.StatsView : _screen.WalletView;

            var statsPresenter = _projectPresentersFactory.CreateStatsListPresenter(targetView);
            _childPresenters.Add(statsPresenter);
        }

        private void OnResetStatsButtonClicked()
        {
            _statsController.ResetStatsForGold();
        }

        private void OnDigitsButtonClicked()
        {
            _gameModeChooseService.SelectDigits();
        }

        private void OnLeterstButtonClicked()
        {
            _gameModeChooseService.SelectLetters();
        }
    }
}