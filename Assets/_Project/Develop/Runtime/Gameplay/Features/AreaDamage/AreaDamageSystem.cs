using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore.Systems;
using Assets._Project.Develop.Runtime.Gameplay.Features.ApplyDamage;
using Assets._Project.Develop.Runtime.Utilities;
using Assets._Project.Develop.Runtime.Utilities.Conditions;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.AreaDamage
{
    public class AreaDamageSystem : IInitializableSystem, IDisposableSystem, IUpdatableSystem
    {
        private readonly CollidersRegistryService _collidersRegistryService;
        private Entity _entity;
        private Rigidbody _rigidbody;
        private ReactiveVariable<float> _damage;
        private ReactiveVariable<float> _radius;
        private LayerMask _targetsMask;
        private Buffer<Collider> _collidersBuffer;
        private ReactiveEvent _runAreaDamageEvent;
        private ICompositeCondition _canRunAreaDamage;
        private List<Entity> _targetsToDamage;
        private IDisposable _runDisposable;
        private int _debugSegments = 32;

        public AreaDamageSystem(CollidersRegistryService collidersRegistryService)
        {
            _collidersRegistryService = collidersRegistryService;
        }

        public void OnInit(Entity entity)
        {
            _entity = entity;
            _rigidbody = entity.Rigidbody;

            _damage = entity.AreaDamageValue;
            _radius = entity.AreaDamageRadius;
            _targetsMask = entity.AreaDamageTargetsMask;
            _collidersBuffer = entity.AreaDamageCollidersBuffer;

            _runAreaDamageEvent = entity.RunAreaDamageEvent;
            _canRunAreaDamage = entity.CanRunAreaDamage;

            _targetsToDamage = new List<Entity>(_collidersBuffer.Items.Length);

            _runDisposable = _runAreaDamageEvent.Subscribe(OnRunAreaDamage);
        }

        public void OnDispose()
        {
            _runDisposable.Dispose();
        }

        public void OnUpdate(float deltaTime)
        {
            DrawRadius();
        }

        private void OnRunAreaDamage()
        {
            if (_canRunAreaDamage.Evaluate() == false)
                return;

            _collidersBuffer.Count = Physics.OverlapSphereNonAlloc(
                _rigidbody.position,
                _radius.Value,
                _collidersBuffer.Items,
                _targetsMask,
                QueryTriggerInteraction.Ignore);

            CollectTargets();

            foreach (Entity target in _targetsToDamage)
                target.TakeDamageRequest.Invoke(_damage.Value);

            Debug.Log("Урон получили: " + _targetsToDamage.Count);
        }

        private void CollectTargets()
        {
            _targetsToDamage.Clear();

            for (int i = 0; i < _collidersBuffer.Count; i++)
            {
                Collider collider = _collidersBuffer.Items[i];

                Entity target = _collidersRegistryService.GetBy(collider);

                if (target == null)
                    continue;

                if (target == _entity)
                    continue;

                if (target.HasComponent<TakeDamageRequest>() == false)
                    continue;

                if (_targetsToDamage.Contains(target))
                    continue;

                _targetsToDamage.Add(target);
            }
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