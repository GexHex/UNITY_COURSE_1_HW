using Assets._Project.Develop.Runtime.Configs.Gameplay.Levels;
using Assets._Project.Develop.Runtime.Gameplay.Features.PlayerInteraction;
using Assets._Project.Develop.Runtime.Gameplay.Features.StagesFeature;
using Assets._Project.Develop.Runtime.Utilities.StateMachineCore;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.States
{
    public class RestState : State, IUpdatableState
    {
        private readonly RestCycleService _restCycleService;
        private readonly PlayerService _playerService;
        private readonly LevelConfig _levelConfig;

        public RestState(
            RestCycleService restCycleService,
            PlayerService playerService,
            LevelConfig levelConfig)
        {
            _restCycleService = restCycleService;
            _playerService = playerService;
            _levelConfig = levelConfig;
        }

        public override void Enter()
        {
            base.Enter();

            float restDuration = _levelConfig.RestDuration > 0.1f ? _levelConfig.RestDuration : 8f;

            Debug.Log($"Отдых {restDuration:0.0} сек: Ставь мины - ЛКМ");

            _playerService.SetMode(PlayerModes.PlaceMine);
            _restCycleService.Begin(restDuration);
        }

        public void Update(float deltaTime)
        {
        }

        public override void Exit()
        {
            base.Exit();

            _restCycleService.Cleanup();
            _playerService.SetMode(PlayerModes.None);
        }
    }
}
