using Assets._Project.Develop.Runtime.Infrastructure.DI;
using Assets._Project.Develop.Runtime.Meta.Features;
using Assets._Project.Develop.Runtime.Meta.Features.ScoreCounter;
using Assets._Project.Develop.Runtime.Meta.Features.Stats;
using Assets._Project.Develop.Runtime.Meta.Features.Wallet;
using Assets._Project.Develop.Runtime.Utilities.ConfigsManagment;
using Assets._Project.Develop.Runtime.Utilities.CoroutinesManagment;
using Assets._Project.Develop.Runtime.Utilities.SceneManagment;
using Assets._Project.Develop.Runtime.Utilities.UI;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Meta.Infrastructure
{
    public class MainMenuContextRegistrations
    {
        public static void Process(DIContainer container)
        {
            Debug.Log("Процесс регистрации сервисов на сцене меню");

            container.RegisterAsSingle(CreateGameModeChooseService);
            container.RegisterAsSingle(CreateStatsInfo);
            container.RegisterAsSingle(CreateStatsController);
        }

        private static GameModeChooseService CreateGameModeChooseService(DIContainer c)
        {
            SceneSwitcherService sceneSwitcherService = c.Resolve<SceneSwitcherService>();
            ICoroutinesPerformer iCoroutinesPerformer = c.Resolve<ICoroutinesPerformer>();

            return new GameModeChooseService(sceneSwitcherService, iCoroutinesPerformer);
        }

        private static StatsInfo CreateStatsInfo(DIContainer c)
        {
            return new StatsInfo();
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
    }
}