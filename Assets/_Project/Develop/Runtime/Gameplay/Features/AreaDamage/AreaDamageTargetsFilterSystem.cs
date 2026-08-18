using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore.Systems;
using Assets._Project.Develop.Runtime.Utilities;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using System;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.AreaDamage
{
    public class AreaDamageTargetsFilterSystem : IInitializableSystem, IDisposableSystem
    {
        private readonly CollidersRegistryService _collidersRegistryService;

        private Entity _entity;
        private Buffer<Collider> _collidersBuffer;
        private Buffer<Entity> _targetsBuffer;
        private ReactiveEvent _runAreaDamageEvent;

        private IDisposable _disposable;

        public AreaDamageTargetsFilterSystem(CollidersRegistryService collidersRegistryService)
        {
            _collidersRegistryService = collidersRegistryService;
        }

        public void OnInit(Entity entity)
        {
            _entity = entity;
            _collidersBuffer = entity.AreaDamageCollidersBuffer;
            _targetsBuffer = entity.AreaDamageTargetsBuffer;
            _runAreaDamageEvent = entity.RunAreaDamageEvent;

            _disposable = _runAreaDamageEvent.Subscribe(CollectTargets);
        }

        public void OnDispose()
        {
            _disposable.Dispose();
        }

        private void CollectTargets()
        {
            _targetsBuffer.Count = 0;

            for (int i = 0; i < _collidersBuffer.Count; i++)
            {
                Collider collider = _collidersBuffer.Items[i];

                Entity target = _collidersRegistryService.GetBy(collider);

                if (target == null)
                    continue;

                if (target == _entity)
                    continue;

                if (Contains(target))
                    continue;

                _targetsBuffer.Items[_targetsBuffer.Count] = target;
                _targetsBuffer.Count++;
            }
        }

        private bool Contains(Entity entity)
        {
            for (int i = 0; i < _targetsBuffer.Count; i++)
                if (_targetsBuffer.Items[i] == entity)
                    return true;

            return false;
        }
    }
}