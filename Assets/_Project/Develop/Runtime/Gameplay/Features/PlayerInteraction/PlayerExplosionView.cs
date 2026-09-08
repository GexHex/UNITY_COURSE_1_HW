using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore.Mono;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using System;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.PlayerInteraction
{
    public class PlayerExplosionView : EntityView
    {
        [SerializeField] private ParticleSystem _explosionEffectPrefab;

        private ReactiveEvent<Vector3> _playerExplosionEvent;

        private IDisposable _playerExplosionDisposable;

        protected override void OnEntityStartedWork(Entity entity)
        {
            _playerExplosionEvent = entity.PlayerExplosionEvent;

            _playerExplosionDisposable = _playerExplosionEvent.Subscribe(OnExploded);
        }

        public override void Cleanup(Entity entity)
        {
            base.Cleanup(entity);

            _playerExplosionDisposable.Dispose();
        }

        private void OnExploded(Vector3 position)
        {
            Instantiate(_explosionEffectPrefab, position, Quaternion.identity);
        }
    }
}
