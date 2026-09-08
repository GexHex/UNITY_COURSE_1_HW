using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore.Mono;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using System;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.Attack
{
    public class AttackExplosionView : EntityView
    {
        [SerializeField] private ParticleSystem _explosionEffectPrefab;
        [SerializeField] private Transform _effectSpawnPoint;

        private ReactiveEvent _attackDelayEndEvent;

        private IDisposable _attackDelayEndDisposable;

        protected override void OnEntityStartedWork(Entity entity)
        {
            _attackDelayEndEvent = entity.AttackDelayEndEvent;
            _attackDelayEndDisposable = _attackDelayEndEvent.Subscribe(OnAttackDelayEnd);
        }

        public override void Cleanup(Entity entity)
        {
            base.Cleanup(entity);

            _attackDelayEndDisposable.Dispose();
        }

        private void OnAttackDelayEnd()
        {
            Instantiate(_explosionEffectPrefab, _effectSpawnPoint.position, Quaternion.identity);
        }
    }
}
