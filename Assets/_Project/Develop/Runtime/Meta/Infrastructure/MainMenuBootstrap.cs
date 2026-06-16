using Assets._Project.Develop.Runtime.Gameplay.Infrastructure;
using Assets._Project.Develop.Runtime.Infrastructure;
using Assets._Project.Develop.Runtime.Infrastructure.DI;
using Assets._Project.Develop.Runtime.UI.Wrappers;
using Assets._Project.Develop.Runtime.Utilities.CoroutinesManagment;
using Assets._Project.Develop.Runtime.Utilities.SceneManagment;
using System.Collections;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Meta.Infrastructure
{
    public class MainMenuBootstrap : SceneBootstrap
    {
        [SerializeField] private UIButtonWrapper _digits;
        [SerializeField] private UIButtonWrapper _letters;
        private GameplayInputArgs _inputArgs;

        private DIContainer _container;

        private void Awake()
        {
            _digits.Clicked += PressDigits;
            _letters.Clicked += PressLetters;
        }

        private void OnDestroy()
        {
            _digits.Clicked -= PressDigits;
            _letters.Clicked -= PressLetters;
        }

        public override void ProcessRegistrations(DIContainer container, IInputSceneArgs sceneArgs = null)
        {
            _container = container;

            MainMenuContextRegistrations.Process(_container);
        }

        public override IEnumerator Initialize()
        {
            Debug.Log("Инициализация сцены меню");

            yield break;
        }

        public override void Run()
        {
            Debug.Log("Старт сцены меню");
        }

        private void PressDigits()
        {
            Debug.Log("Нажата кнопка цифр");

            _inputArgs = new(true);
            ChangeLevel(_inputArgs);
        }

        private void PressLetters()
        {
            Debug.Log("Нажата кнопка букв");

            _inputArgs = new(false);
            ChangeLevel(_inputArgs);
        }

        private void ChangeLevel(IInputSceneArgs gameplayInputArgs)
        {
            SceneSwitcherService sceneSwitcherService = _container.Resolve<SceneSwitcherService>();
            ICoroutinesPerformer coroutinesPerformer = _container.Resolve<ICoroutinesPerformer>();
            coroutinesPerformer.StartPerform(sceneSwitcherService.ProcessSwitchTo(Scenes.Gameplay, gameplayInputArgs));
        }
    }
}