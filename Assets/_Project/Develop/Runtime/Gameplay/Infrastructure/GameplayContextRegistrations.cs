using Assets._Project.Develop.Runtime.Configs.Meta.Level;
using Assets._Project.Develop.Runtime.Gameplay.Level;
using Assets._Project.Develop.Runtime.Infrastructure.DI;
using Assets._Project.Develop.Runtime.Meta.Features.ScoreCounter;
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
        }

        private static UserInputService CreateUserInputService(DIContainer c)
        {
            //Debug.Log("Зарегистрирован --- UserInputService");

            return new UserInputService();
        }

        private static RandomGeneratorService CreateRandomGeneratorService(DIContainer c)
        {
            //Debug.Log("Зарегистрирован --- RandomGeneratorService");

            return new RandomGeneratorService();
        }

        private static GameplayCycle CreateGameCycle(DIContainer c)
        {
            //Debug.Log("Зарегистрирован --- GameplayCycle");

            GameplayInputArgs args = c.Resolve<GameplayInputArgs>();

            LevelConfig levelConfig = c.Resolve<ConfigsProviderService>().GetConfig<LevelConfig>();

            RandomGeneratorService randomGeneratorService = c.Resolve<RandomGeneratorService>();

            UserInputService userInputService = c.Resolve<UserInputService>();

            SceneSwitcherService sceneSwitcherService = c.Resolve<SceneSwitcherService>();

            ICoroutinesPerformer coroutinesPerformer = c.Resolve<ICoroutinesPerformer>();

            StatsService statsService = c.Resolve<StatsService>();

            PlayerDataProvider playerDataProvider = c.Resolve<PlayerDataProvider>();

            return new GameplayCycle(coroutinesPerformer,
                args,
                levelConfig,
                randomGeneratorService,
                userInputService,
                sceneSwitcherService,
                statsService,
                playerDataProvider);
        }
    }
}