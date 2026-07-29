using Assets._Project.Develop.Runtime.Gameplay.Common;
using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore.Systems;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.MovementFeature
{
    internal class CharacterControllerMovementSystem : IInitializableSystem, IUpdatableSystem
    {
        private ReactiveVariable<Vector3> _moveDirection;
        private ReactiveVariable<float> _moveSpeed;
        private CharacterController _characterController;
        private Transform _transform;

        public void OnInit(Entity entity)
        {
            _moveDirection = entity.GetComponent<MoveDirection>().Value;
            _moveSpeed = entity.GetComponent<MoveSpeed>().Value;
            _characterController = entity.GetComponent<CharacterControllerComponent>().Value;
            _transform = entity.GetComponent<TransformComponent>().Value;
        }

        public void OnUpdate(float deltaTime)
        {
            if (_moveDirection.Value.sqrMagnitude < 0.0001f)
            {
                return;
            }

            Vector3 worldDirection = _transform.rotation * _moveDirection.Value.normalized;

            _characterController.Move(worldDirection * _moveSpeed.Value * deltaTime);
        }
    }
}
