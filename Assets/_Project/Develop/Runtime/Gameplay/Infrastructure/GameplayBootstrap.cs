using Assets._Project.Develop.Runtime.Configs.Gameplay.Levels;
using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Gameplay.Features.AI;
using Assets._Project.Develop.Runtime.Gameplay.Features.BuildingFeature;
using Assets._Project.Develop.Runtime.Gameplay.Features.PlayerInteraction;
using Assets._Project.Develop.Runtime.Gameplay.States;
using Assets._Project.Develop.Runtime.Infrastructure;
using Assets._Project.Develop.Runtime.Infrastructure.DI;
using Assets._Project.Develop.Runtime.UI.Gameplay;
using Assets._Project.Develop.Runtime.Utilities.SceneManagment;
using System;
using System.Collections;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Infrastructure
{
    public class GameplayBootstrap : SceneBootstrap
    {
        private DIContainer _container;
        private GameplayInputArgs _inputArgs;

        private GameplayStatesContext _gameplayStatesContext;
        private EntitiesLifeContext _entitiesLifeContext;
        private AIBrainsContext _brainsContext;
        private PlayerService _playerService;
        private GameplayScreenPresenter _screenPresenter;

        public override void ProcessRegistrations(DIContainer container, IInputSceneArgs sceneArgs = null)
        {
            _container = container;

            if (sceneArgs is not GameplayInputArgs gameplayInputArgs)
                throw new ArgumentException($"{nameof(sceneArgs)} is not match with {typeof(GameplayInputArgs)} type");

            _inputArgs = gameplayInputArgs;

            GameplayContextRegistrations.Process(_container, _inputArgs);
        }

        public override IEnumerator Initialize()
        {
            Debug.Log($"Вы попали на уровень {_inputArgs.LevelNumber}");

            Debug.Log("Инициализация геймплейной сцены");

            _entitiesLifeContext = _container.Resolve<EntitiesLifeContext>();
            _brainsContext = _container.Resolve<AIBrainsContext>();
            _gameplayStatesContext = _container.Resolve<GameplayStatesContext>();
            _playerService = _container.Resolve<PlayerService>();
            _screenPresenter = _container.Resolve<GameplayScreenPresenter>();

            LevelConfig levelConfig = _container.Resolve<LevelConfig>();

            _container.Resolve<BuildingFactory>().Create(
                Vector3.zero,
                levelConfig.TowerMaxHealth,
                levelConfig.TowerConfig);

            _playerService.Initialize();


            yield break;
        }

        public override void Run()
        {
            Debug.Log("Старт геймплейной сцены");

            _gameplayStatesContext.Run();
        }

        private void Update()
        {
            _brainsContext?.Update(Time.deltaTime);
            _entitiesLifeContext?.Update(Time.deltaTime);
            _playerService?.Update();
            _gameplayStatesContext?.Update(Time.deltaTime);
        }

        private void LateUpdate()
        {
            _screenPresenter?.LateUpdate();
        }
    }
}
