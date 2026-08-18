using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore.Systems;
using Assets._Project.Develop.Runtime.Utilities.Conditions;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.MovementFeature
{
    public class RigidbodyMovementSystem : IInitializableSystem, IUpdatableSystem
    {
        private ReactiveVariable<Vector3> _moveDirection;
        private ReactiveVariable<float> _moveSpeed;
        private Rigidbody _rigidbody;
        private ICompositeCondition _canMove;
        private ReactiveVariable<bool> _isMoving;

        public void OnInit(Entity entity)
        {
            _moveDirection = entity.MoveDirection;
            _moveSpeed = entity.MoveSpeed;
            _rigidbody = entity.Rigidbody;
            _canMove = entity.CanMove;
            _isMoving = entity.IsMoving;
        }

        public void OnUpdate(float deltaTime)
        {
            bool canMove = _canMove.Evaluate();
            bool hasDirection = _moveDirection.Value.sqrMagnitude > 0.0001f;

            _isMoving.Value = canMove && hasDirection;

            if (_isMoving.Value == false)
            {
                _rigidbody.velocity = Vector3.zero;

                return;
            }

            _rigidbody.velocity = _moveDirection.Value.normalized * _moveSpeed.Value;
        }
    }
}
