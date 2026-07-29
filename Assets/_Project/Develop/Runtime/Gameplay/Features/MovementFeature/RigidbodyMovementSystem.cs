using Assets._Project.Develop.Runtime.Gameplay.Common;
using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore.Systems;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.MovementFeature
{
    public class RigidbodyMovementSystem : IInitializableSystem, IFixedUpdatableSystem
    {
        private ReactiveVariable<Vector3> _moveDirection;
        private ReactiveVariable<float> _moveSpeed;
        private Rigidbody _rigidbody;

        public void OnInit(Entity entity)
        {
            _moveDirection = entity.GetComponent<MoveDirection>().Value;
            _moveSpeed = entity.GetComponent<MoveSpeed>().Value;
            _rigidbody = entity.GetComponent<RigidbodyComponent>().Value;
        }      

        public void OnFixedUpdate(float deltaTime)
        {
            Vector3 input = _moveDirection.Value;

            if (input.sqrMagnitude < 0.0001f)
            {
                _rigidbody.velocity = new Vector3(0f, _rigidbody.velocity.y, 0f);
                return;
            }

            Vector3 worldDirection = _rigidbody.rotation * input.normalized;

            Vector3 velocity = worldDirection * _moveSpeed.Value;

            _rigidbody.velocity = velocity;
        }
    }
}
