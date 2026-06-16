using Assets._Project.Develop.Runtime.Gameplay.Infrastructure;
using Assets._Project.Develop.Runtime.Infrastructure.DI;
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
        
        RandomGeneratorService _randomGeneratorService;
        UserInputService _userInputService;

        private GameMode _gameMode;

        private DIContainer _container;
        private GameplayInputArgs _inputArgs;
        private LevelConfig _levelConfig;

        private List<char> _userAnswers;
        private List<char> _generateRightAnswers;
        private bool _isUserInputFinish;

        private int _answerLength = 5;


        public GameplayCycle(DIContainer container, IInputSceneArgs sceneArgs, LevelConfig levelConfig)
        {
            _container = container;
            _inputArgs = (GameplayInputArgs)sceneArgs;
            _levelConfig = levelConfig;
        }

        public IEnumerator Launch()
        {
            _randomGeneratorService = _container.Resolve<RandomGeneratorService>();
            _userInputService = _container.Resolve<UserInputService>();

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

            _userInputService.Update();
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
            GameCycleEnded?.Invoke(_inputArgs);

            _gameMode.Win -= OnGameModeWin;
            _gameMode.Defeat -= OnGameModeDefeat;
            _userInputService.InputCompleted -= OnInputCompleted;
        }

        public void Dispose()
        {
            OnGameModeEnded();
        }
    }
}