using Assets._Project.Develop.Runtime.Configs.Meta.Level;
using Assets._Project.Develop.Runtime.Gameplay.Level;
using Assets._Project.Develop.Runtime.Infrastructure.DI;
using Assets._Project.Develop.Runtime.Meta.Features;
using Assets._Project.Develop.Runtime.Meta.Features.ScoreCounter;
using Assets._Project.Develop.Runtime.Meta.Features.Stats;
using Assets._Project.Develop.Runtime.Meta.Features.Wallet;
using Assets._Project.Develop.Runtime.UI.Core;
using Assets._Project.Develop.Runtime.Utilities.AssetsManagment;
using Assets._Project.Develop.Runtime.Utilities.ConfigsManagment;
using Assets._Project.Develop.Runtime.Utilities.CoroutinesManagment;
using Assets._Project.Develop.Runtime.Utilities.DataManagment.DataProviders;
using Assets._Project.Develop.Runtime.Utilities.Generators;
using Assets._Project.Develop.Runtime.Utilities.SceneManagment;
using Assets._Project.Develop.Runtime.Utilities.UserInput;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Infrastructure
{
    public class GameplayContextRegistrations
    {
        public static void Process(DIContainer container, GameplayInputArgs inputArgs)
        {
            Debug.Log("Процесс регистрации сервисов на сцене геймплея");

            container.RegisterAsSingle(CreateRandomGeneratorService);
            container.RegisterAsSingle(CreateUserInputService);
            container.RegisterAsSingle(CreateGameCycle);
            container.RegisterAsSingle(c => inputArgs);
            container.RegisterAsSingle(CreateGameplayUIRoot).NonLazy();
            container.RegisterAsSingle(CreateGameplayPresentersFactory);
            container.RegisterAsSingle(CreateStatsController);
            container.RegisterAsSingle(CreateGameplayScreenPresenter).NonLazy();
            container.RegisterAsSingle(CreateStatsInfo);
        }

        private static UserInputService CreateUserInputService(DIContainer c)
        {
            return new UserInputService();
        }

        private static RandomGeneratorService CreateRandomGeneratorService(DIContainer c)
        {
            return new RandomGeneratorService();
        }

        private static GameplayCycle CreateGameCycle(DIContainer c)
        {
            GameplayInputArgs args = c.Resolve<GameplayInputArgs>();

            ConfigsProviderService configsProviderService = c.Resolve<ConfigsProviderService>();

            LevelConfig levelConfig = configsProviderService.GetConfig<LevelConfig>();

            RandomGeneratorService randomGeneratorService = c.Resolve<RandomGeneratorService>();

            UserInputService userInputService = c.Resolve<UserInputService>();

            SceneSwitcherService sceneSwitcherService = c.Resolve<SceneSwitcherService>();

            ICoroutinesPerformer coroutinesPerformer = c.Resolve<ICoroutinesPerformer>();

            StatsService statsService = c.Resolve<StatsService>();

            WalletService walletService = c.Resolve<WalletService>();

            PlayerDataProvider playerDataProvider = c.Resolve<PlayerDataProvider>();

            return new GameplayCycle(
                coroutinesPerformer,
                args,
                levelConfig,
                randomGeneratorService,
                userInputService,
                sceneSwitcherService,
                statsService,
                walletService,
                configsProviderService,
                playerDataProvider);
        }

        private static GameplayUIRoot CreateGameplayUIRoot(DIContainer c)
        {
            ResourcesAssetsLoader resourcesAssetsLoader = c.Resolve<ResourcesAssetsLoader>();

            GameplayUIRoot gmeplayUIRootPrefab = resourcesAssetsLoader
                .Load<GameplayUIRoot>("UI/GamePlay/GamePlayUIRoot");

            return Object.Instantiate(gmeplayUIRootPrefab);
        }

        private static GameplayPresentersFactory CreateGameplayPresentersFactory(DIContainer c)
        {
            return new GameplayPresentersFactory(c);
        }

        private static GameplayScreenPresenter CreateGameplayScreenPresenter(DIContainer c)
        {
            GameplayUIRoot uiRoot = c.Resolve<GameplayUIRoot>();

            GameplayScreenView view = c
                .Resolve<ViewsFactory>()
                .Create<GameplayScreenView>(ViewIDs.GameplayScreen, uiRoot.HUDLayer);

            GameplayScreenPresenter presenter = c
                .Resolve<GameplayPresentersFactory>()
                .CreateGameplayScreen(view);

            return presenter;
        }

        private static StatsController CreateStatsController(DIContainer c)
        {
            return new StatsController(
                c.Resolve<StatsService>(),
                c.Resolve<WalletService>(),
                c.Resolve<ConfigsProviderService>(),
                c.Resolve<StatsInfo>()
            );
        }

        private static StatsInfo CreateStatsInfo(DIContainer c)
        {
            return new StatsInfo();
        }
    }
}