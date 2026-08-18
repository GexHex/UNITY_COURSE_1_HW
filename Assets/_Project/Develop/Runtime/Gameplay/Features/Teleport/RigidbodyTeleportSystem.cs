using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore.Systems;
using Assets._Project.Develop.Runtime.Utilities.Conditions;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using System;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.Teleport
{
    public class RigidbodyTeleportSystem : IInitializableSystem, IDisposableSystem, IUpdatableSystem
    {
        private Rigidbody _rigidbody;
        private ReactiveEvent<Vector3> _runTeleportEvent;
        private ReactiveEvent _completedTeleportEvent;
        private ReactiveVariable<float> _teleportRadius;
        private ICompositeCondition _canTeleport;
        private IDisposable _disposable;

        private int _debugSegments = 32;

        public void OnInit(Entity entity)
        {
            _rigidbody = entity.Rigidbody;
            _runTeleportEvent = entity.RunTeleportEvent;
            _completedTeleportEvent = entity.CompletedTeleportEvent;
            _canTeleport = entity.CanTeleport;
            _teleportRadius = entity.TeleportRadius;

            _disposable = _runTeleportEvent.Subscribe(Teleport);
        }
        public void OnDispose()
        {
            _disposable.Dispose();
        }

        public void Teleport(Vector3 requestedPosition)
        {
            if (_canTeleport.Evaluate() == false)
            {
                Debug.Log("Телепорт невозможен");

                return;
            }

            Vector3 center = _rigidbody.position;

            Vector3 offset = requestedPosition - center;
            offset.y = 0f;

            float radius = Mathf.Max(0f, _teleportRadius.Value);

            if (offset.sqrMagnitude > radius * radius)
                offset = offset.normalized * radius;

            _rigidbody.position = center + offset;

            Physics.SyncTransforms();  //!!!

            _completedTeleportEvent.Invoke();
        }

        public void OnUpdate(float deltaTime)
        {
            DrawRadius();
        }

        private void DrawRadius()
        {
            Vector3 center = _rigidbody.position;

            float radius = Mathf.Max(0f, _teleportRadius.Value);

            for (int i = 0; i < _debugSegments; i++)
            {
                float angleA = (float)i / _debugSegments * Mathf.PI * 2f;
                float angleB = (float)(i + 1) / _debugSegments * Mathf.PI * 2f;

                Vector3 pointA = center + new Vector3(Mathf.Cos(angleA), 0f, Mathf.Sin(angleA)) * radius;
                Vector3 pointB = center + new Vector3(Mathf.Cos(angleB), 0f, Mathf.Sin(angleB)) * radius;

                Debug.DrawLine(pointA, pointB, Color.white);
            }
        }
    }
}