using Assets._Project.Develop.Runtime.Gameplay.Common;
using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore.Systems;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.RotationFeature
{
    internal class CharacterControllerRotationSystem : IInitializableSystem, IUpdatableSystem
    {
        private ReactiveVariable<float> _rotationDirection;
        private ReactiveVariable<float> _rotationSpeed;
        private Transform _transform;

        public void OnInit(Entity entity)
        {
            _rotationDirection = entity.GetComponent<RotationDirection>().Value;
            _rotationSpeed = entity.GetComponent<RotationSpeed>().Value;
            _transform = entity.GetComponent<TransformComponent>().Value;
        }

        public void OnUpdate(float deltaTime)
        {
            if (Mathf.Abs(_rotationDirection.Value) < 0.0001f)
                return;

            _transform.Rotate(Vector3.up, _rotationDirection.Value * _rotationSpeed.Value * deltaTime);
        }
    }
}
