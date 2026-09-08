using Assets._Project.Develop.Runtime.Gameplay.Features.PlayerInteraction;
using Assets._Project.Develop.Runtime.Gameplay.Features.StagesFeature;
using Assets._Project.Develop.Runtime.Utilities.StateMachineCore;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.States
{
    public class WaveProcessState : State, IUpdatableState
    {
        private readonly StageProviderService _stageProviderService;
        private readonly PlayerService _playerService;

        public WaveProcessState(
            StageProviderService stageProviderService,
            PlayerService playerService)
        {
            _stageProviderService = stageProviderService;
            _playerService = playerService;
        }

        public override void Enter()
        {
            base.Enter();

            Debug.Log("Волна началась: клик создаёт взрыв");
            _playerService.SetMode(PlayerModes.Explode);
            _stageProviderService.SwitchToNext();
            _stageProviderService.StartCurrent();
        }

        public void Update(float deltaTime)
        {
            _stageProviderService.UpdateCurrent(deltaTime);
        }

        public override void Exit()
        {
            base.Exit();

            _stageProviderService.CleanupCurrent();
            _playerService.SetMode(PlayerModes.None);
        }
    }
}
