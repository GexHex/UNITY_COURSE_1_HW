using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore.Systems;
using Assets._Project.Develop.Runtime.Utilities;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using System;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.Attack.Shoot
{
    public class InstantShootSystem : IInitializableSystem, IDisposableSystem
    {
        private ReactiveEvent _attackDelayEndEvent;

        private Entity _entity;

        private ReactiveVariable<float> _damage;

        private Buffer<Entity> _contacts;

        private IDisposable _attackDelayEndDisposable;

        public void OnInit(Entity entity)
        {
            _entity = entity;

            _attackDelayEndEvent = entity.AttackDelayEndEvent;

            _damage = entity.InstantAttackDamage;

            _contacts = entity.ContactEntitiesBuffer;

            _attackDelayEndDisposable = _attackDelayEndEvent.Subscribe(OnAttackDelayEnd);
        }

        private void OnAttackDelayEnd()
        {
            for (int i = 0; i < _contacts.Count; i++)
                EntitiesHelper.TryTakeDamageFrom(_entity, _contacts.Items[i], _damage.Value);
        }

        public void OnDispose()
        {
            _attackDelayEndDisposable.Dispose();
        }
    }
}
