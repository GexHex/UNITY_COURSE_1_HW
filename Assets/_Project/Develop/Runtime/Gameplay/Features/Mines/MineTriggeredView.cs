using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore.Mono;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using System;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.Mines
{
    public class MineTriggeredView : EntityView
    {
        [SerializeField] private ParticleSystem _explosionEffectPrefab;
        [SerializeField] private Transform _effectSpawnPoint;

        private ReactiveEvent _mineTriggeredEvent;

        private IDisposable _mineTriggeredDisposable;

        protected override void OnEntityStartedWork(Entity entity)
        {
            _mineTriggeredEvent = entity.MineTriggeredEvent;

            _mineTriggeredDisposable = _mineTriggeredEvent.Subscribe(OnTriggered);
        }

        public override void Cleanup(Entity entity)
        {
            base.Cleanup(entity);

            _mineTriggeredDisposable.Dispose();
        }

        private void OnTriggered()
        {
            Instantiate(_explosionEffectPrefab, _effectSpawnPoint.position, Quaternion.identity);
        }
    }
}
