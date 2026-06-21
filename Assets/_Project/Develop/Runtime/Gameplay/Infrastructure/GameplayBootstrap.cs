using Assets._Project.Develop.Runtime.Infrastructure;
using Assets._Project.Develop.Runtime.Infrastructure.DI;
using Assets._Project.Develop.Runtime.Utilities.CoroutinesManagment;
using Assets._Project.Develop.Runtime.Utilities.SceneManagment;
using System;
using System.Collections;
using UnityEngine;
using Assets._Project.Develop.Runtime.Gameplay.Level;

namespace Assets._Project.Develop.Runtime.Gameplay.Infrastructure
{
    public class GameplayBootstrap : SceneBootstrap
    {
        private DIContainer _container;
        private GameplayInputArgs _inputArgs;
        private GameplayCycle _gameCycle;

        public override void ProcessRegistrations(DIContainer container, IInputSceneArgs sceneArgs)
        {
            _container = container;

            if (sceneArgs is not GameplayInputArgs gameplayInputArgs)
                throw new ArgumentException($"{nameof(sceneArgs)} is not match with {typeof(GameplayInputArgs)} type");

            _inputArgs = gameplayInputArgs;          

            GameplayContextRegistrations.Process(_container, _inputArgs);
        }

        public override IEnumerator Initialize()
        {
            Debug.Log("Инициализация геймплейной сцены");

            _gameCycle = _container.Resolve<GameplayCycle>();

            _gameCycle.GameCycleEnded += ChangeLevel;

            yield break;
        }

        public override void Run()
        {
            Debug.Log("Старт геймплейной сцены");

            _container.Resolve<ICoroutinesPerformer>().StartPerform(_gameCycle.Launch());           
        }    

        private void Update()
        {
            _gameCycle?.Update();
        }

        private void OnDestroy()
        {
            if (_gameCycle != null)
            {
                _gameCycle.GameCycleEnded -= ChangeLevel;
                _gameCycle.Dispose();
            }
        }

        private void ChangeLevel(IInputSceneArgs gameplayInputArgs)
        {
            SceneSwitcherService sceneSwitcherService = _container.Resolve<SceneSwitcherService>();
            ICoroutinesPerformer coroutinesPerformer = _container.Resolve<ICoroutinesPerformer>();
            coroutinesPerformer.StartPerform(sceneSwitcherService.ProcessSwitchTo(Scenes.MainMenu, _inputArgs));
        }
    }
}