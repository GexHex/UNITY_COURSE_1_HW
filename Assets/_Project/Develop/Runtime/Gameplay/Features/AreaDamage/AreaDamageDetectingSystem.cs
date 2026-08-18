using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore.Systems;
using Assets._Project.Develop.Runtime.Utilities;
using Assets._Project.Develop.Runtime.Utilities.Conditions;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using System;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.AreaDamage
{
    public class AreaDamageDetectingSystem : IInitializableSystem, IDisposableSystem, IUpdatableSystem
    {
        private Rigidbody _rigidbody;
        private ReactiveVariable<float> _radius;
        private LayerMask _targetsMask;
        private Buffer<Collider> _collidersBuffer;
        private ReactiveEvent _runAreaDamageEvent;
        private ICompositeCondition _canRunAreaDamage;
        private IDisposable _disposable;

        private int _debugSegments = 32;

        public void OnInit(Entity entity)
        {
            _rigidbody = entity.Rigidbody;
            _radius = entity.AreaDamageRadius;
            _targetsMask = entity.AreaDamageTargetsMask;
            _collidersBuffer = entity.AreaDamageCollidersBuffer;
            _runAreaDamageEvent = entity.RunAreaDamageEvent;
            _canRunAreaDamage = entity.CanRunAreaDamage;

            _disposable = _runAreaDamageEvent.Subscribe(DetectTargets);
        }

        private void DetectTargets()
        {
            if (_canRunAreaDamage.Evaluate() == false)
            {
                _collidersBuffer.Count = 0;

                return;
            }

            _collidersBuffer.Count = Physics.OverlapSphereNonAlloc(
                _rigidbody.position,
                _radius.Value,
                _collidersBuffer.Items,
                _targetsMask,
                QueryTriggerInteraction.Ignore);
        }

        public void OnDispose()
        {
            _disposable.Dispose();
        }

        public void OnUpdate(float deltaTime)
        {
            DrawRadius();
        }

        private void DrawRadius()
        {
            Vector3 center = _rigidbody.position;

            for (int i = 0; i < _debugSegments; i++)
            {
                float angleA = (float)i / _debugSegments * Mathf.PI * 2f;
                float angleB = (float)(i + 1) / _debugSegments * Mathf.PI * 2f;

                Vector3 pointA = center + new Vector3(Mathf.Cos(angleA), 0f, Mathf.Sin(angleA)) * _radius.Value;
                Vector3 pointB = center + new Vector3(Mathf.Cos(angleB), 0f, Mathf.Sin(angleB)) * _radius.Value;

                Debug.DrawLine(pointA, pointB, Color.red);
            }
        }
    }
}