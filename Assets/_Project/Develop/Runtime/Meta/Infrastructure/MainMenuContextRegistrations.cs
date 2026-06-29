using Assets._Project.Develop.Runtime.Infrastructure.DI;
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
        }

        private static GameModeChooseService CreateGameModeChooseService(DIContainer c)
        {
            SceneSwitcherService sceneSwitcherService = c.Resolve<SceneSwitcherService>();

            ICoroutinesPerformer iCoroutinesPerformer = c.Resolve<ICoroutinesPerformer>();

            return new GameModeChooseService(sceneSwitcherService, iCoroutinesPerformer);
        }
    }
}
