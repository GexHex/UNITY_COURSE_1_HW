using Assets._Project.Develop.Runtime.Configs.Meta.Level;
using Assets._Project.Develop.Runtime.Gameplay.Infrastructure;
using Assets._Project.Develop.Runtime.Meta.Features.ScoreCounter;
using Assets._Project.Develop.Runtime.Meta.Features.Wallet;
using Assets._Project.Develop.Runtime.Utilities.ConfigsManagment;
using Assets._Project.Develop.Runtime.Utilities.CoroutinesManagment;
using Assets._Project.Develop.Runtime.Utilities.DataManagment.DataProviders;
using Assets._Project.Develop.Runtime.Utilities.Generators;
using Assets._Project.Develop.Runtime.Utilities.SceneManagment;
using Assets._Project.Develop.Runtime.Utilities.UserInput;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Level
{
    public class GameplayCycle : IDisposable
    {
        private readonly RandomGeneratorService _randomGeneratorService;
        private readonly PlayerDataProvider _playerDataProvider;
        private readonly UserInputService _userInputService;
        private readonly StatsService _statService;
        private readonly WalletService _walletService;
        private readonly ConfigsProviderService _configsProviderService;
        private readonly ICoroutinesPerformer _coroutinesPerformer;
        private readonly SceneSwitcherService _sceneSwitcherService;

        private GameMode _gameMode;
        private GameplayInputArgs _inputArgs;
        private LevelConfig _levelConfig;
        private List<char> _userAnswers;
        private List<char> _generateRightAnswers;
        private bool _isUserInputFinish;
        private int _answerLength = 2;

        public GameplayCycle(
             ICoroutinesPerformer coroutinesPerformer,
             IInputSceneArgs sceneArgs,
             LevelConfig levelConfig,
             RandomGeneratorService randomGeneratorService,
             UserInputService userInputService,
             SceneSwitcherService sceneSwitcherService,
             StatsService statService,
             WalletService walletService,
             ConfigsProviderService configsProviderService,
             PlayerDataProvider playerDataProvider)
        {
            _coroutinesPerformer = coroutinesPerformer;
            _inputArgs = (GameplayInputArgs)sceneArgs;
            _levelConfig = levelConfig;
            _randomGeneratorService = randomGeneratorService;
            _userInputService = userInputService;
            _sceneSwitcherService = sceneSwitcherService;
            _statService = statService;
            _walletService = walletService;
            _configsProviderService = configsProviderService;
            _playerDataProvider = playerDataProvider;
        }

        public IEnumerator Launch()
        {
            _gameMode = new();

            _gameMode.Win += OnGameModeWin;
            _gameMode.Defeat += OnGameModeDefeat;
            _userInputService.InputCompleted += OnInputCompleted;

            _generateRightAnswers = _randomGeneratorService.GenerateRightAnswers(_inputArgs.IsDigits, _levelConfig, _answerLength);

            Debug.Log($"---------------------------------------");

            if (_inputArgs.IsDigits == true)
                _userInputService.StartDigitsInput();
            else
                _userInputService.StartLettersInput();

            yield return null;
        }

        public void Update()
        {
            if (_isUserInputFinish == true)
                return;

            _userInputService?.Update();
        }

        private void OnInputCompleted(List<char> userAnswers)
        {
            _isUserInputFinish = true;
            _userAnswers = userAnswers;

            _gameMode.CheckResult(_generateRightAnswers, _userAnswers);
        }

        private void OnGameModeDefeat()
        {
            Debug.Log("---------------------------------------");
            Debug.Log("*************** DEFEAT! ***************");
            Debug.Log("---------------------------------------");

            _statService.AddLoss();

            int goldToLose = _configsProviderService.GetConfig<GameBalanceConfig>().GoldToLose;
            if (_walletService.Enough(CurrencyTypes.Gold, goldToLose))
                _walletService.Spend(CurrencyTypes.Gold, goldToLose);
            else
                Debug.Log("Нет золота для удаления!");

            OnGameModeEnded();
        }

        private void OnGameModeWin()
        {
            Debug.Log("---------------------------------------");
            Debug.Log("***************** WIN! ****************");
            Debug.Log("---------------------------------------");

            _statService.AddWin();

            int goldToWin = _configsProviderService.GetConfig<GameBalanceConfig>().GoldToWin;
            _walletService.Add(CurrencyTypes.Gold, goldToWin);

            OnGameModeEnded();
        }

        private void OnGameModeEnded()
        {
            _coroutinesPerformer.StartPerform(EndGameProcess());

            _gameMode.Win -= OnGameModeWin;
            _gameMode.Defeat -= OnGameModeDefeat;
            _userInputService.InputCompleted -= OnInputCompleted;
        }

        private IEnumerator EndGameProcess()
        {
            yield return _coroutinesPerformer.StartPerform(_playerDataProvider.Save());

            Debug.Log("Статус сохранен");

            yield return _coroutinesPerformer.StartPerform(_sceneSwitcherService.ProcessSwitchTo(Scenes.MainMenu, _inputArgs));
        }

        public void Dispose()
        {
            if (_gameMode != null)
            {
                _gameMode.Win -= OnGameModeWin;
                _gameMode.Defeat -= OnGameModeDefeat;
            }
            if (_userInputService != null)
            {
                _userInputService.InputCompleted -= OnInputCompleted;
            }
        }
    }
}