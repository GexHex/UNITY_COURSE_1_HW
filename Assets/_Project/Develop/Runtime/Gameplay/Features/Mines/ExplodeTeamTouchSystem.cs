using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore.Systems;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.Mines
{
    public class ExplodeTeamTouchSystem : IInitializableSystem, IUpdatableSystem
    {
        private readonly EntitiesLifeContext _entitiesLifeContext;
        private Entity _entity;
        private Transform _transform;
        private ReactiveVariable<bool> _isTouchAnotherTeam;
        private ReactiveVariable<float> _explosionRadius;
        private ReactiveVariable<float> _explosionDamage;
        private ReactiveEvent _mineTriggeredEvent;
        private bool _exploded;

        public ExplodeTeamTouchSystem(EntitiesLifeContext entitiesLifeContext)
        {
            _entitiesLifeContext = entitiesLifeContext;
        }

        public void OnInit(Entity entity)
        {
            _entity = entity;
            _transform = entity.Transform;
            _isTouchAnotherTeam = entity.IsTouchAnotherTeam;
            _explosionRadius = entity.ExplosionRadius;
            _explosionDamage = entity.ExplosionDamage;
            _mineTriggeredEvent = entity.MineTriggeredEvent;
        }

        public void OnUpdate(float deltaTime)
        {
            if (_exploded)
                return;

            if (_isTouchAnotherTeam.Value == false)
                return;

            _exploded = true;

            _mineTriggeredEvent.Invoke();

            float sqrRadius = _explosionRadius.Value * _explosionRadius.Value;

            for (int i = 0; i < _entitiesLifeContext.Entities.Count; i++)
            {
                Entity target = _entitiesLifeContext.Entities[i];

                if (target == _entity || target.IsInit == false)
                    continue;

                if (target.TryGetTransform(out Transform transform) == false)
                    continue;

                if ((transform.position - _transform.position).sqrMagnitude > sqrRadius)
                    continue;

                EntitiesHelper.TryTakeDamageFrom(_entity, target, _explosionDamage.Value);
            }
        }
    }
}
