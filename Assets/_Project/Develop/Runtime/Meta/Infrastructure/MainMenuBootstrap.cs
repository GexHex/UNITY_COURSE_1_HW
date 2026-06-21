using Assets._Project.Develop.Runtime.Infrastructure;
using Assets._Project.Develop.Runtime.Infrastructure.DI;
using Assets._Project.Develop.Runtime.UI.Menu;
using Assets._Project.Develop.Runtime.Utilities.CoroutinesManagment;
using Assets._Project.Develop.Runtime.Utilities.SceneManagment;
using Assets._Project.Develop.Runtime.Utilities.UI;
using System.Collections;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Meta.Infrastructure
{
    public class MainMenuBootstrap : SceneBootstrap
    {
        [SerializeField] private UIMainMenu _uiMainMenu;

        private GameModeChooseService _gameModeChooseService;

        private DIContainer _container;

        public override void ProcessRegistrations(DIContainer container, IInputSceneArgs sceneArgs = null)
        {
            _container = container;

            MainMenuContextRegistrations.Process(_container);
        }

        public override IEnumerator Initialize()
        {
            Debug.Log("Инициализация сцены меню");         

            _gameModeChooseService = _container.Resolve<GameModeChooseService>();

            _uiMainMenu.DigitsClicked += _gameModeChooseService.SelectDigits;
            _uiMainMenu.LettersClicked += _gameModeChooseService.SelectLetters;

            _gameModeChooseService.GameModeSelected += ChangeLevel;

            yield break;
        }

        public override void Run()
        {
            Debug.Log("Старт сцены меню");
        }

        private void OnDestroy()
        {
            if (_gameModeChooseService == null)
                return;

            _uiMainMenu.DigitsClicked -= _gameModeChooseService.SelectDigits;
            _uiMainMenu.LettersClicked -= _gameModeChooseService.SelectLetters;
        }

        private void ChangeLevel(IInputSceneArgs gameplayInputArgs)
        {
            SceneSwitcherService sceneSwitcherService = _container.Resolve<SceneSwitcherService>();
            ICoroutinesPerformer coroutinesPerformer = _container.Resolve<ICoroutinesPerformer>();
            coroutinesPerformer.StartPerform(sceneSwitcherService.ProcessSwitchTo(Scenes.Gameplay, gameplayInputArgs));
        }
    }
}