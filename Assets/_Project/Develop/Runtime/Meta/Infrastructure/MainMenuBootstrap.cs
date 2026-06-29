using Assets._Project.Develop.Runtime.Infrastructure;
using Assets._Project.Develop.Runtime.Infrastructure.DI;
using Assets._Project.Develop.Runtime.Meta.Features.ScoreCounter;
using Assets._Project.Develop.Runtime.UI.Menu;
using Assets._Project.Develop.Runtime.Utilities.SceneManagment;
using Assets._Project.Develop.Runtime.Utilities.UI;
using System.Collections;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Meta.Infrastructure
{
    public class MainMenuBootstrap : SceneBootstrap
    {
        [SerializeField] private UIMainMenu _uiMainMenu;

        private DIContainer _container;
        private StatsService _statsService;
        private GameModeChooseService _gameModeChooseService;

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

            _statsService = _container.Resolve<StatsService>();

            yield break;
        }

        public override void Run()
        {
            Debug.Log("Старт сцены меню");

            _statsService.Run();
        }     

        private void Update()
        {
            _statsService?.Update();            
        } 

        private void OnDestroy()
        {
            if (_gameModeChooseService == null)
                return;
            
            _uiMainMenu.DigitsClicked -= _gameModeChooseService.SelectDigits;
            _uiMainMenu.LettersClicked -= _gameModeChooseService.SelectLetters;
        }
    }
}