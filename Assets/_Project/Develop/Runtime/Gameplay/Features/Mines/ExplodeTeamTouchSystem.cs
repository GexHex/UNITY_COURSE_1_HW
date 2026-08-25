using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore.Systems;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.Mines
{
    public class ExplodeTeamTouchSystem : IInitializableSystem, IUpdatableSystem
    {
        private readonly EntitiesFactory _entitiesFactory;

        private Entity _entity;
        private Transform _transform;
        private ReactiveVariable<bool> _isTouchAnotherTeam;
        private ReactiveVariable<float> _explosionRadius;
        private ReactiveVariable<float> _explosionDamage;

        private bool _exploded;

        public ExplodeTeamTouchSystem(EntitiesFactory entitiesFactory)
        {
            _entitiesFactory = entitiesFactory;
        }

        public void OnInit(Entity entity)
        {
            _entity = entity;
            _transform = entity.Transform;
            _isTouchAnotherTeam = entity.IsTouchAnotherTeam;
            _explosionRadius = entity.ExplosionRadius;
            _explosionDamage = entity.ExplosionDamage;
        }

        public void OnUpdate(float deltaTime)
        {
            if (_exploded)
                return;

            if (_isTouchAnotherTeam.Value == false)
                return;

            _exploded = true;
            _entitiesFactory.CreateExplosion(
                _transform.position,
                _explosionRadius.Value,
                _explosionDamage.Value,
                _entity);
        }
    }
}
