using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore.Systems;
using Assets._Project.Develop.Runtime.Gameplay.Features.AreaDamage;
using Assets._Project.Develop.Runtime.Utilities.Conditions;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using System;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.ApplyDamage
{
    public class ApplyDamageSystem : IInitializableSystem, IDisposableSystem
    {
        private ReactiveEvent<float> _damageRequest;
        private ReactiveEvent<float> _damageEvent;
        private ReactiveVariable<float> _health;

        private ICompositeCondition _canApplyDamage;
        private IDisposable _requestDisposable;

        private bool _isPlayer;

        public void OnInit(Entity entity)
        {
            _damageRequest = entity.TakeDamageRequest;
            _damageEvent = entity.TakeDamageEvent;
            _health = entity.CurrentHealth;
            _canApplyDamage = entity.CanApplyDamage;

            _isPlayer = entity.HasComponent<AreaDamageValue>();

            _requestDisposable = _damageRequest.Subscribe(OnDamageRequest);
        }

        public void OnDispose()
        {
            _requestDisposable.Dispose();
        }

        private void OnDamageRequest(float damage)
        {
            if (damage < 0)
                throw new ArgumentOutOfRangeException(nameof(damage));

            if (_canApplyDamage.Evaluate() == false)
                return;

            _health.Value = MathF.Max(_health.Value - damage, 0);
            _damageEvent.Invoke(damage);

            string who = _isPlayer
              ? "<color=green>[ИГРОК]</color>"
              : "<color=red>[ВРАГ]</color>";

            Debug.Log($"{who} получил урон: {damage}. Осталось HP: {_health.Value}");
        }
    }
}
