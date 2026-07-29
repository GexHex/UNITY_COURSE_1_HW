using Assets._Project.Develop.Runtime.Gameplay.Common;
using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore.Systems;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.RotationFeature
{
    internal class RigidbodyRotationSystem : IInitializableSystem, IFixedUpdatableSystem
    {
        private ReactiveVariable<float> _rotationDirection;
        private ReactiveVariable<float> _rotationSpeed;
        private Rigidbody _rigidbody;

        public void OnInit(Entity entity)
        {
            _rotationDirection = entity.GetComponent<RotationDirection>().Value;
            _rigidbody = entity.GetComponent<RigidbodyComponent>().Value;
            _rotationSpeed = entity.GetComponent<RotationSpeed>().Value;
        }       

        public void OnFixedUpdate(float fixedDeltaTime)
        {
            if (Mathf.Abs(_rotationDirection.Value) < 0.0001f) 
                return;

            _rigidbody.MoveRotation(_rigidbody.rotation * Quaternion.Euler(0f, _rotationDirection.Value * _rotationSpeed.Value * fixedDeltaTime, 0f));
        }
    }
}