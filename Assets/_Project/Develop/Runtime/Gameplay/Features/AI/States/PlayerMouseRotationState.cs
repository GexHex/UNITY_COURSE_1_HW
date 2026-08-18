using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Gameplay.Features.InputFeature;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using Assets._Project.Develop.Runtime.Utilities.StateMachineCore;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.AI.States
{
    public class PlayerMouseRotationState : State, IUpdatableState
    {
        private readonly IInputService _inputService;
        private readonly Transform _transform;
        private readonly ReactiveVariable<Vector3> _movementDirection;
        private readonly ReactiveVariable<Vector3> _rotationDirection;
        private readonly float _degreesPerMouseUnit;

        public PlayerMouseRotationState(Entity entity, IInputService inputService, float degreesPerMouseUnit = 12f)
        {
            _inputService = inputService;

            _transform = entity.Transform;
            _movementDirection = entity.MoveDirection;
            _rotationDirection = entity.RotationDirection;

            _degreesPerMouseUnit = degreesPerMouseUnit;
        }

        public override void Enter()
        {
            base.Enter();

            _movementDirection.Value = Vector3.zero;
            _rotationDirection.Value = Vector3.zero;
        }

        public void Update(float deltaTime)
        {
            float mouseDelta = _inputService.HorizontalLookDelta;

            if (Mathf.Abs(mouseDelta) <= 0.0001f)
            {
                _rotationDirection.Value = Vector3.zero;

                return;
            }

            Quaternion turn = Quaternion.Euler(0f, mouseDelta * _degreesPerMouseUnit, 0f);
            _rotationDirection.Value = turn * _transform.forward;
        }

        public override void Exit()
        {
            base.Exit();

            _rotationDirection.Value = Vector3.zero;
        }
    }
}
