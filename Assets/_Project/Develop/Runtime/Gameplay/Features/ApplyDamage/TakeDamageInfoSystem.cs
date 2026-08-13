using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore.Systems;
using Assets._Project.Develop.Runtime.Gameplay.Features.Player;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using System;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.ApplyDamage
{
    public class TakeDamageInfoSystem : IInitializableSystem, IDisposableSystem
    {
        private ReactiveEvent<float> _takeDamageEvent;
        private ReactiveVariable<float> _health;
        private bool _isPlayer;

        private IDisposable _takeDamagedisposable;

        public void OnInit(Entity entity)
        {
            _takeDamageEvent = entity.TakeDamageEvent;
            _health = entity.CurrentHealth;
            _isPlayer = entity.HasComponent<IsPlayer>();

            _takeDamagedisposable = _takeDamageEvent.Subscribe(OnTakeDamage);
        }

        public void OnDispose()
        {
            _takeDamagedisposable.Dispose();
        }

        private void OnTakeDamage(float damage)
        {
            string entity = _isPlayer
                ? "<color=green>[ИГРОК]</color>"
                : "<color=red>[ВРАГ]</color>";

            Debug.Log($"{entity} Получил урон: {damage}. Осталось HP: {_health.Value}");
        }
    }
}