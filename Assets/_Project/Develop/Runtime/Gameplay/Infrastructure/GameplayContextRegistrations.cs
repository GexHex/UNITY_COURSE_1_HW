using Assets._Project.Develop.Runtime.Configs.Gameplay.Levels;
using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore.Mono;
using Assets._Project.Develop.Runtime.Gameplay.Features.AI;
using Assets._Project.Develop.Runtime.Gameplay.Features.BuildingFeature;
using Assets._Project.Develop.Runtime.Gameplay.Features.Enemies;
using Assets._Project.Develop.Runtime.Gameplay.Features.InputFeature;
using Assets._Project.Develop.Runtime.Gameplay.Features.PlayerInteraction;
using Assets._Project.Develop.Runtime.Gameplay.Features.StagesFeature;
using Assets._Project.Develop.Runtime.Gameplay.States;
using Assets._Project.Develop.Runtime.Infrastructure.DI;
using Assets._Project.Develop.Runtime.Meta.Features.Wallet;
using Assets._Project.Develop.Runtime.UI;
using Assets._Project.Develop.Runtime.UI.Core;
using Assets._Project.Develop.Runtime.UI.Gameplay;
using Assets._Project.Develop.Runtime.Utilities.AssetsManagment;
using Assets._Project.Develop.Runtime.Utilities.ConfigsManagment;
using Assets._Project.Develop.Runtime.Utilities.Timer;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Infrastructure
{
    public class GameplayContextRegistrations
    {
        private static GameplayInputArgs _inputArgs;

        public static void Process(DIContainer container, GameplayInputArgs args)
        {
            _inputArgs = args;

            container.RegisterAsSingle(CreateLevelConfig);

            container.RegisterAsSingle(CreateEntitiesFactory);

            container.RegisterAsSingle(CreateEntitiesLifeContext);

            container.RegisterAsSingle(CreateCollidersRegistryService);

            container.RegisterAsSingle(CreateBrainsFactory);

            container.RegisterAsSingle(CreateAIBrainsContext);

            container.RegisterAsSingle(CreateBuildingFactory);
            container.RegisterAsSingle(CreateEnemiesFactory);

            container.RegisterAsSingle(CreateStagesFactory);
            container.RegisterAsSingle(CreateStageProviderService);

            container.RegisterAsSingle(CreateRestCycleService);

            container.RegisterAsSingle(CreateGameplayStatesFactory);

            container.RegisterAsSingle(CreateGameplayStatesContext);

            container.RegisterAsSingle(CreateBuildingHolderService).NonLazy();

            container.RegisterAsSingle<IInputService>(CreateDesktopInput);

            container.RegisterAsSingle(CreatePlayerService);

            container.RegisterAsSingle(CreateMonoEntitiesFactory).NonLazy();

            container.RegisterAsSingle(CreateGameplayUIRoot).NonLazy();
            container.RegisterAsSingle(CreateGameplayPresentersFactory);
            container.RegisterAsSingle(CreateGameplayScreenPresenter).NonLazy();

            container.RegisterAsSingle(CreateGameplayPopupService);
        }

        private static GameplayPopupService CreateGameplayPopupService(DIContainer c)
        {
            return new GameplayPopupService(
                c.Resolve<ViewsFactory>(),
                c.Resolve<ProjectPresentersFactory>(),
                c.Resolve<GameplayUIRoot>(),
                c.Resolve<GameplayPresentersFactory>());
        }

        private static LevelConfig CreateLevelConfig(DIContainer c)
        {
            return c.Resolve<ConfigsProviderService>()
                .GetConfig<LevelsListConfig>()
                .GetBy(_inputArgs.LevelNumber);
        }

        private static PlayerService CreatePlayerService(DIContainer c)
        {
            return new PlayerService(
                c.Resolve<IInputService>(),
                c.Resolve<EntitiesFactory>(),
                c.Resolve<WalletService>(),
                c.Resolve<LevelConfig>(),
                c.Resolve<EntitiesLifeContext>());
        }

        private static RestCycleService CreateRestCycleService(DIContainer c)
        {
            return new RestCycleService(c.Resolve<TimerServiceFactory>());
        }

        private static GameplayStatesContext CreateGameplayStatesContext(DIContainer c)
        {
            return new GameplayStatesContext(c.Resolve<GameplayStatesFactory>().CreateGameplayStateMachine());
        }

        private static GameplayStatesFactory CreateGameplayStatesFactory(DIContainer c)
        {
            return new GameplayStatesFactory(c);
        }

        private static BuildingHolderService CreateBuildingHolderService(DIContainer c)
        {
            return new BuildingHolderService(c.Resolve<EntitiesLifeContext>());
        }

        private static StageProviderService CreateStageProviderService(DIContainer c)
        {
            return new StageProviderService(
                c.Resolve<LevelConfig>(),
                c.Resolve<StagesFactory>());
        }

        private static StagesFactory CreateStagesFactory(DIContainer c)
        {
            return new StagesFactory(c);
        }

        private static EnemiesFactory CreateEnemiesFactory(DIContainer c)
        {
            return new EnemiesFactory(c);
        }

        private static BuildingFactory CreateBuildingFactory(DIContainer c)
        {
            return new BuildingFactory(c);
        }

        private static DesktopInput CreateDesktopInput(DIContainer c)
        {
            return new DesktopInput();
        }

        private static AIBrainsContext CreateAIBrainsContext(DIContainer c)
        {
            return new AIBrainsContext();
        }

        private static BrainsFactory CreateBrainsFactory(DIContainer c)
        {
            return new BrainsFactory(c);
        }

        private static CollidersRegistryService CreateCollidersRegistryService(DIContainer c)
        {
            return new CollidersRegistryService();
        }

        private static MonoEntitiesFactory CreateMonoEntitiesFactory(DIContainer c)
        {
            return new MonoEntitiesFactory(
                c.Resolve<ResourcesAssetsLoader>(),
                c.Resolve<EntitiesLifeContext>(),
                c.Resolve<CollidersRegistryService>());
        }

        private static EntitiesLifeContext CreateEntitiesLifeContext(DIContainer c)
        {
            return new EntitiesLifeContext();
        }

        private static EntitiesFactory CreateEntitiesFactory(DIContainer c)
        {
            return new EntitiesFactory(c);
        }

        private static GameplayUIRoot CreateGameplayUIRoot(DIContainer c)
        {
            ResourcesAssetsLoader resourcesAssetsLoader = c.Resolve<ResourcesAssetsLoader>();

            GameplayUIRoot prefab = resourcesAssetsLoader.Load<GameplayUIRoot>("UI/Gameplay/GameplayUIRoot");

            return Object.Instantiate(prefab);
        }

        private static GameplayPresentersFactory CreateGameplayPresentersFactory(DIContainer c)
        {
            return new GameplayPresentersFactory(c, _inputArgs);
        }

        private static GameplayScreenPresenter CreateGameplayScreenPresenter(DIContainer c)
        {
            GameplayUIRoot uiRoot = c.Resolve<GameplayUIRoot>();

            GameplayScreenView view = c
                .Resolve<ViewsFactory>()
                .Create<GameplayScreenView>(ViewIDs.GameplayScreen, uiRoot.HUDLayer);

            //GameplayScreenPresenter presenter = c
            //   .Resolve<GameplayPresentersFactory>()
            //   .CreateGameplayScreenPresenter(view);

            //return presenter;
            return c.Resolve<GameplayPresentersFactory>().CreateGameplayScreen(view);
        }
    }
}
