using Assets._Project.Develop.Runtime.Gameplay.Infrastructure;
using Assets._Project.Develop.Runtime.Utilities.CoroutinesManagment;
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
        public Action<GameplayInputArgs> GameCycleEnded;
       
        private readonly SceneSwitcherService _sceneSwitcherService;
        private readonly ICoroutinesPerformer _coroutinesPerformer;

        RandomGeneratorService _randomGeneratorService;
        UserInputService _userInputService;

        private GameMode _gameMode;

        private GameplayInputArgs _inputArgs;
        private LevelConfig _levelConfig;

        private List<char> _userAnswers;
        private List<char> _generateRightAnswers;
        private bool _isUserInputFinish;

        private int _answerLength = 5;


        public GameplayCycle(
            ICoroutinesPerformer coroutinesPerformer,
            IInputSceneArgs sceneArgs,
            LevelConfig levelConfig,
            RandomGeneratorService randomGeneratorService,
            UserInputService userInputService,
            SceneSwitcherService sceneSwitcherService)
        {
            _coroutinesPerformer = coroutinesPerformer;
            _inputArgs = (GameplayInputArgs)sceneArgs;
            _levelConfig = levelConfig;
            _randomGeneratorService = randomGeneratorService;
            _userInputService = userInputService;
            _sceneSwitcherService = sceneSwitcherService;
        }

        public IEnumerator Launch()
        {
            _gameMode = new();

            _gameMode.Win += OnGameModeWin;
            _gameMode.Defeat += OnGameModeDefeat;
            _userInputService.InputCompleted += OnInputCompleted;

            _generateRightAnswers = _randomGeneratorService.GenerateRightAnswers(_inputArgs.IsDigits, _levelConfig, _answerLength);

            if (_inputArgs.IsDigits == true)
                _userInputService.StartDigitsInput();
            else
                _userInputService.StartLettersInput();

            Debug.Log($"====================");

            yield return null;
        }

        public void Update()
        {
            if (_isUserInputFinish == true)
                return;

            _userInputService?.Update();
        }

        private void OnInputCompleted(List<char> userUnswers)
        {
            _isUserInputFinish = true;
            _userAnswers = userUnswers;

            _gameMode.CheckResult(_generateRightAnswers, _userAnswers);
        }

        private void OnGameModeDefeat()
        {
            Debug.Log("****** DEFEAT! ******");

            OnGameModeEnded();
        }

        private void OnGameModeWin()
        {
            Debug.Log("******** WIN! *******");

            OnGameModeEnded();
        }

        private void OnGameModeEnded()
        {
            _coroutinesPerformer.StartPerform(_sceneSwitcherService.ProcessSwitchTo(Scenes.MainMenu, _inputArgs));

            _gameMode.Win -= OnGameModeWin;
            _gameMode.Defeat -= OnGameModeDefeat;
            _userInputService.InputCompleted -= OnInputCompleted;
        }

        public void Dispose()
        {
            _gameMode.Win -= OnGameModeWin;
            _gameMode.Defeat -= OnGameModeDefeat;            
            _userInputService.InputCompleted -= OnInputCompleted;
        }
    }
}