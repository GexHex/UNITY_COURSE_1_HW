using Assets._Project.Develop.Runtime.Gameplay.Features.InputFeature;
using Assets._Project.Develop.Runtime.Gameplay.Features.PlayerInteraction;
using Assets._Project.Develop.Runtime.Meta.Features.Statistics;
using Assets._Project.Develop.Runtime.UI.Gameplay;
using Assets._Project.Develop.Runtime.Utilities.CoroutinesManagment;
using Assets._Project.Develop.Runtime.Utilities.DataManagment.DataProviders;
using Assets._Project.Develop.Runtime.Utilities.SceneManagment;
using Assets._Project.Develop.Runtime.Utilities.StateMachineCore;
using System.Collections;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.States
{
    public class DefeatState : EndGameState, IUpdatableState
    {
        private readonly GameStatsService _statsService;
        private readonly GameplayPopupService _popupService;
        private readonly PlayerDataProvider _playerDataProvider;
        private readonly ICoroutinesPerformer _coroutinesPerformer;

        public DefeatState(
            IInputService inputService,
            PlayerService playerService,
            GameStatsService statisticsService,
            PlayerDataProvider playerDataProvider,
            ICoroutinesPerformer coroutinesPerformer,
            GameplayPopupService popupService) : base(inputService, playerService)
        {
            _statsService = statisticsService;
            _popupService = popupService;
            _playerDataProvider = playerDataProvider;
            _coroutinesPerformer = coroutinesPerformer;
        }

        public override void Enter()
        {
            base.Enter();

            Debug.Log("ПОРАЖЕНИЕ!");

            _statsService.RegisterLoss();

            _popupService.OpenDefeatPopup();

            _coroutinesPerformer.StartPerform(ReturnToMenu());
        }

        public void Update(float deltaTime)
        {
        }

        private IEnumerator ReturnToMenu()
        {
            yield return _playerDataProvider.SaveAsync();
            yield return new WaitForSeconds(1.5f);
        }
    }
}
