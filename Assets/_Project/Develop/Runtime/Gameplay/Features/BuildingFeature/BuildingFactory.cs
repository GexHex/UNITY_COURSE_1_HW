using Assets._Project.Develop.Runtime.Configs.Gameplay.Entities;
using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Gameplay.Features.TeamsFeature;
using Assets._Project.Develop.Runtime.Infrastructure.DI;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.BuildingFeature
{
    public class BuildingFactory
    {
        private readonly EntitiesFactory _entitiesFactory;
        private readonly EntitiesLifeContext _entitiesLifeContext;

        public BuildingFactory(DIContainer container)
        {
            _entitiesFactory = container.Resolve<EntitiesFactory>();
            _entitiesLifeContext = container.Resolve<EntitiesLifeContext>();
        }

        public Entity Create(Vector3 position, float maxHealth, TowerConfig config)
        {
            Entity entity = _entitiesFactory.CreateBuilding(position, maxHealth, config);

            entity
                .AddIsBuilding()
                .AddTeam(new ReactiveVariable<Teams>(Teams.Buildings));

            _entitiesLifeContext.Add(entity);

            return entity;
        }
    }
}
