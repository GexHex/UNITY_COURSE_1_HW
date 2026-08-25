using Assets._Project.Develop.Runtime.Gameplay.Features.InputFeature;
using Assets._Project.Develop.Runtime.Gameplay.Features.PlayerInteraction;
using Assets._Project.Develop.Runtime.Utilities.StateMachineCore;

namespace Assets._Project.Develop.Runtime.Gameplay.States
{
    public abstract class EndGameState : State
    {
        private readonly IInputService _inputService;
        private readonly PlayerService _playerService;

        protected EndGameState(
            IInputService inputService,
            PlayerService playerService)
        {
            _inputService = inputService;
            _playerService = playerService;
        }

        public override void Enter()
        {
            base.Enter();

            _inputService.IsEnabled = false;
            _playerService.SetMode(PlayerModes.None);
        }

        public override void Exit()
        {
            base.Exit();

            _inputService.IsEnabled = true;
        }
    }
}
