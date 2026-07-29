using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore.Mono;
using Assets._Project.Develop.Runtime.Gameplay.Features.MovementFeature;
using Assets._Project.Develop.Runtime.Gameplay.Features.RotationFeature;
using Assets._Project.Develop.Runtime.Infrastructure.DI;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.EntitiesCore
{
    public class EntitiesFactory
    {
        private readonly DIContainer _container;
        private readonly EntitiesLifeContext _entitiesLifeContext;

        private readonly MonoEntitiesFactory _monoEntitiesFactory;

        public EntitiesFactory(DIContainer container)
        {
            _container = container;
            _entitiesLifeContext = _container.Resolve<EntitiesLifeContext>();
            _monoEntitiesFactory = _container.Resolve<MonoEntitiesFactory>();
        }

        public Entity CreateTestRigidbodyEntity(Vector3 position)
        {
            Entity entity = CreateEmpty();

            _monoEntitiesFactory.Create(entity, position, "Entities/RigidbodyEntity");

            entity
                .AddComponent(new MoveDirection() { Value = new ReactiveVariable<Vector3>(Vector3.zero) })
                .AddComponent(new MoveSpeed() { Value = new ReactiveVariable<float>(10) })
                .AddComponent(new RotationDirection() { Value = new ReactiveVariable<float>(0f) })
                .AddComponent(new RotationSpeed() { Value = new ReactiveVariable<float>(500f) });

            entity.AddSystem(new RigidbodyRotationSystem());
            entity.AddSystem(new RigidbodyMovementSystem());

            _entitiesLifeContext.Add(entity);

            return entity;
        }

        public Entity CreateTestCharacterControllerEntity(Vector3 position)
        {
            Entity entity = CreateEmpty();

            _monoEntitiesFactory.Create(entity, position, "Entities/CharacterControllerEntity");

            entity
                .AddComponent(new MoveDirection() { Value = new ReactiveVariable<Vector3>(Vector3.zero) })
                .AddComponent(new MoveSpeed() { Value = new ReactiveVariable<float>(10) })
                .AddComponent(new RotationDirection() { Value = new ReactiveVariable<float>(0f) })
                .AddComponent(new RotationSpeed() { Value = new ReactiveVariable<float>(300f) });

            entity.AddSystem(new CharacterControllerMovementSystem());
            entity.AddSystem(new CharacterControllerRotationSystem());

            _entitiesLifeContext.Add(entity);

            return entity;
        }

        private Entity CreateEmpty() => new Entity();
    }
}
