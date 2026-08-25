using Assets._Project.Develop.Runtime.Configs.Gameplay.Levels;
using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Gameplay.Features.BuildingFeature;
using Assets._Project.Develop.Runtime.Gameplay.Features.PlayerInteraction;
using Assets._Project.Develop.Runtime.Gameplay.Features.StagesFeature;
using Assets._Project.Develop.Runtime.UI.Core;
using Assets._Project.Develop.Runtime.UI.Wallet;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.UI.Gameplay
{
    public class GameplayScreenPresenter : IPresenter
    {
        private readonly GameplayScreenView _view;
        private readonly ProjectPresentersFactory _projectPresentersFactory;
        private readonly BuildingHolderService _buildingHolderService;
        private readonly StageProviderService _stageProviderService;
        private readonly RestCycleService _restCycleService;
        private readonly PlayerService _playerService;
        private readonly LevelConfig _levelConfig;

        private readonly List<IPresenter> _childPresenters = new();

        private IDisposable _healthDisposable;
        private IDisposable _waveDisposable;
        private IDisposable _restDisposable;
        private IDisposable _restingDisposable;
        private IDisposable _modeDisposable;
        private IDisposable _buildingRegistredDisposable;

        public GameplayScreenPresenter(
            GameplayScreenView view,
            ProjectPresentersFactory projectPresentersFactory,
            BuildingHolderService buildingHolderService,
            StageProviderService stageProviderService,
            RestCycleService restCycleService,
            PlayerService playerService,
            LevelConfig levelConfig)
        {
            _view = view;
            _projectPresentersFactory = projectPresentersFactory;
            _buildingHolderService = buildingHolderService;
            _stageProviderService = stageProviderService;
            _restCycleService = restCycleService;
            _playerService = playerService;
            _levelConfig = levelConfig;
        }

        public void Initialize()
        {
            WalletPresenter walletPresenter = _projectPresentersFactory.CreateWalletPresenter(_view.WalletView);
            _childPresenters.Add(walletPresenter);

            foreach (IPresenter presenter in _childPresenters)
                presenter.Initialize();

            if (_buildingHolderService.Building != null)
                BindBuilding(_buildingHolderService.Building);
            else
                _buildingRegistredDisposable = _buildingHolderService.BuildingRegistred.Subscribe(BindBuilding);

            _waveDisposable = _stageProviderService.CurrentStageNumber.Subscribe(OnWaveChanged);
            UpdateWave(_stageProviderService.CurrentStageNumber.Value);

            _restDisposable = _restCycleService.RemainingTime.Subscribe(OnRestTimeChanged);
            _restingDisposable = _restCycleService.IsResting.Subscribe(OnRestingChanged);
            UpdateRest();

            _modeDisposable = _playerService.Mode.Subscribe(OnModeChanged);
            UpdateMode(_playerService.Mode.Value);
        }

        public void Dispose()
        {
            _healthDisposable?.Dispose();
            _waveDisposable?.Dispose();
            _restDisposable?.Dispose();
            _restingDisposable?.Dispose();
            _modeDisposable?.Dispose();
            _buildingRegistredDisposable?.Dispose();

            foreach (IPresenter presenter in _childPresenters)
                presenter.Dispose();

            _childPresenters.Clear();
        }

        private void BindBuilding(Entity building)
        {
            _healthDisposable?.Dispose();
            _healthDisposable = building.CurrentHealth.Subscribe(OnHealthChanged);
            UpdateHealth(building.CurrentHealth.Value);
        }

        private void OnHealthChanged(float oldValue, float newValue) => UpdateHealth(newValue);

        private void UpdateHealth(float value)
        {
            _view.BuildingHealthView.SetText($"Жизни: {Mathf.RoundToInt(value)}/{Mathf.RoundToInt(_levelConfig.TowerMaxHealth)}");
        }

        private void OnWaveChanged(int oldValue, int newValue) => UpdateWave(newValue);

        private void UpdateWave(int value)
        {
            _view.WaveView.SetText($"Волна: {value}/{_stageProviderService.StagesCount}");
        }

        private void OnRestTimeChanged(float oldValue, float newValue) => UpdateRest();

        private void OnRestingChanged(bool oldValue, bool newValue) => UpdateRest();

        private void UpdateRest()
        {
            if (_restCycleService.IsResting.Value)
                _view.RestTimerView.SetText($"Отдых {_restCycleService.RemainingTime.Value:0}");
            else
                _view.RestTimerView.SetText("Атака");
        }

        private void OnModeChanged(PlayerModes oldValue, PlayerModes newValue)
            => UpdateMode(newValue);

        private void UpdateMode(PlayerModes mode)
        {
            switch (mode)
            {
                case PlayerModes.PlaceMine:
                    _view.ModeView.SetText($"Устанавливай мины ЛКМ, цена: {_levelConfig.MineConfig.Cost}");
                    break;

                case PlayerModes.Explode:
                    _view.ModeView.SetText("Взрывай мобов ЛКМ");
                    break;

                default:
                    _view.ModeView.SetText("");
                    break;
            }
        }
    }
}
