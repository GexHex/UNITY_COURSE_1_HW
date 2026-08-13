using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore.Systems;
using Assets._Project.Develop.Runtime.Utilities.Conditions;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.Teleport
{
    public class RigidbodyTeleportSystem : IInitializableSystem, IDisposableSystem, IUpdatableSystem
    {
        private Rigidbody _rigidbody;
        private ReactiveEvent _runTeleportEvent;
        private ReactiveEvent _completedTeleportEvent;
        private ICompositeCondition _canTeleport;
        private float _teleportRadius = 8f;
        private IDisposable _disposable;

        private int _debugSegments = 32;

        public void OnInit(Entity entity)
        {
            _rigidbody = entity.Rigidbody;
            _runTeleportEvent = entity.RunTeleportEvent;
            _completedTeleportEvent = entity.CompletedTeleportEvent;
            _canTeleport = entity.CanTeleport;

            _disposable = _runTeleportEvent.Subscribe(Teleport);
        }
        public void OnDispose()
        {
            _disposable.Dispose();
        }

        public void Teleport()
        {
            if (_canTeleport.Evaluate() == false)
            {
                Debug.Log("Телепорт невозможен");

                return;
            }

            Vector3 center = _rigidbody.position;
            Vector2 offset = Random.insideUnitCircle * _teleportRadius;
            Vector3 spawnPosition = center + new Vector3(offset.x, 0, offset.y);

            _rigidbody.position = spawnPosition;

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

            for (int i = 0; i < _debugSegments; i++)
            {
                float angleA = (float)i / _debugSegments * Mathf.PI * 2f;
                float angleB = (float)(i + 1) / _debugSegments * Mathf.PI * 2f;

                Vector3 pointA = center + new Vector3(Mathf.Cos(angleA), 0f, Mathf.Sin(angleA)) * _teleportRadius;
                Vector3 pointB = center + new Vector3(Mathf.Cos(angleB), 0f, Mathf.Sin(angleB)) * _teleportRadius;

                Debug.DrawLine(pointA, pointB, Color.white);
            }
        }
    }
}