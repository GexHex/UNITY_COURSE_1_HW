using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using Assets._Project.Develop.Runtime.Utilities.StateMachineCore;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.AI.States
{
    public class MoveToTargetState : State, IUpdatableState
    {
        private ReactiveVariable<Vector3> _moveDirection;
        private ReactiveVariable<Vector3> _rotationDirection;
        private ReactiveVariable<Entity> _currentTarget;
        private Transform _transform;

        public MoveToTargetState(Entity entity)
        {
            _moveDirection = entity.MoveDirection;
            _rotationDirection = entity.RotationDirection;
            _currentTarget = entity.CurrentTarget;
            _transform = entity.Transform;
        }

        public void Update(float deltaTime)
        {
            Entity target = _currentTarget.Value;

            if (target == null || target.IsInit == false)
            {
                _moveDirection.Value = Vector3.zero;
                return;
            }

            Vector3 toTarget = target.Transform.position - _transform.position;
            toTarget.y = 0f;

            if (toTarget.sqrMagnitude < 0.0001f)
            {
                _moveDirection.Value = Vector3.zero;
                return;
            }

            Vector3 direction = toTarget.normalized;
            _moveDirection.Value = direction;
            _rotationDirection.Value = direction;
        }

        public override void Exit()
        {
            base.Exit();

            _moveDirection.Value = Vector3.zero;
        }
    }
}
