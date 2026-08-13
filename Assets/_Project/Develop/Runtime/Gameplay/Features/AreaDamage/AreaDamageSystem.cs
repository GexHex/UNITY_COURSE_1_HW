using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore.Systems;
using Assets._Project.Develop.Runtime.Gameplay.Features.ApplyDamage;
using Assets._Project.Develop.Runtime.Utilities;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using System;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.AreaDamage
{
    public class AreaDamageSystem : IInitializableSystem, IDisposableSystem
    {
        private ReactiveVariable<float> _damage;
        private Buffer<Entity> _targetsBuffer;
        private ReactiveEvent _runAreaDamageEvent;

        private IDisposable _disposable;

        public void OnInit(Entity entity)
        {
            _damage = entity.AreaDamageValue;
            _targetsBuffer = entity.AreaDamageTargetsBuffer;
            _runAreaDamageEvent = entity.RunAreaDamageEvent;

            _disposable = _runAreaDamageEvent.Subscribe(DealDamage);
        }

        public void OnDispose()
        {
            _disposable.Dispose();
        }

        private void DealDamage()
        {
            int damagedCount = 0;

            for (int i = 0; i < _targetsBuffer.Count; i++)
            {
                Entity target = _targetsBuffer.Items[i];

                if (target.HasComponent<TakeDamageRequest>() == false)
                    continue;

                target.TakeDamageRequest.Invoke(_damage.Value);

                damagedCount++;
            }

            Debug.Log("Количество врагов получившие урон: " + damagedCount);
        }
    }
}