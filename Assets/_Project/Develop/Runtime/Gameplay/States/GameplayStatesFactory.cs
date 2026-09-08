using Assets._Project.Develop.Runtime.Configs.Gameplay.Levels;
using Assets._Project.Develop.Runtime.Gameplay.Features.BuildingFeature;
using Assets._Project.Develop.Runtime.Gameplay.Features.InputFeature;
using Assets._Project.Develop.Runtime.Gameplay.Features.PlayerInteraction;
using Assets._Project.Develop.Runtime.Gameplay.Features.StagesFeature;
using Assets._Project.Develop.Runtime.Infrastructure.DI;
using Assets._Project.Develop.Runtime.Meta.Features.Statistics;
using Assets._Project.Develop.Runtime.Meta.Features.Wallet;
using Assets._Project.Develop.Runtime.UI.Gameplay;
using Assets._Project.Develop.Runtime.Utilities.Conditions;
using Assets._Project.Develop.Runtime.Utilities.CoroutinesManagment;
using Assets._Project.Develop.Runtime.Utilities.DataManagment.DataProviders;
using Assets._Project.Develop.Runtime.Utilities.SceneManagment;

namespace Assets._Project.Develop.Runtime.Gameplay.States
{
    public class GameplayStatesFactory
    {
        private readonly DIContainer _container;

        public GameplayStatesFactory(DIContainer container)
        {
            _container = container;
        }

        public RestState CreateRestState()
        {
            return new RestState(
                _container.Resolve<RestCycleService>(),
                _container.Resolve<PlayerService>(),
                _container.Resolve<LevelConfig>());
        }

        public WaveProcessState CreateWaveProcessState()
        {
            return new WaveProcessState(
                _container.Resolve<StageProviderService>(),
                _container.Resolve<PlayerService>());
        }

        public WinState CreateWinState()
        {
            return new WinState(
                _container.Resolve<IInputService>(),
                _container.Resolve<PlayerService>(),
                _container.Resolve<GameStatsService>(),
                _container.Resolve<WalletService>(),
                _container.Resolve<LevelConfig>(),
                _container.Resolve<PlayerDataProvider>(),
                _container.Resolve<ICoroutinesPerformer>(),
                _container.Resolve<GameplayPopupService>());
        }

        public DefeatState CreateDefeatState()
        {
            return new DefeatState(
                _container.Resolve<IInputService>(),
                _container.Resolve<PlayerService>(),
                _container.Resolve<GameStatsService>(),
                _container.Resolve<PlayerDataProvider>(),
                _container.Resolve<ICoroutinesPerformer>(),
                _container.Resolve<GameplayPopupService>());
        }

        public GameplayStateMachine CreateGameplayStateMachine()
        {
            StageProviderService stageProviderService = _container.Resolve<StageProviderService>();
            BuildingHolderService buildingHolderService = _container.Resolve<BuildingHolderService>();

            GameplayStateMachine coreLoopState = CreateCoreLoopState();

            DefeatState defeatState = CreateDefeatState();
            WinState winState = CreateWinState();

            ICompositeCondition coreLoopToWinStateCondition = new CompositeCondition()
                .Add(new FuncCondition(() => stageProviderService.CurrentStageResult.Value == StageResults.Completed))
                .Add(new FuncCondition(() => stageProviderService.HasNextStage() == false));

            ICompositeCondition coreLoopToDefeatStateCondition = new CompositeCondition()
                .Add(new FuncCondition(() =>
                {
                    if (buildingHolderService.Building != null)
                        return buildingHolderService.Building.IsDead.Value
                        && buildingHolderService.Building.InDeathProcess.Value == false;

                    return false;
                }));

            GameplayStateMachine gameplayCycle = new GameplayStateMachine();

            gameplayCycle.AddState(coreLoopState);
            gameplayCycle.AddState(winState);
            gameplayCycle.AddState(defeatState);

            gameplayCycle.AddTransition(coreLoopState, winState, coreLoopToWinStateCondition);
            gameplayCycle.AddTransition(coreLoopState, defeatState, coreLoopToDefeatStateCondition);

            return gameplayCycle;
        }

        public GameplayStateMachine CreateCoreLoopState()
        {
            RestCycleService restCycleService = _container.Resolve<RestCycleService>();
            StageProviderService stageProviderService = _container.Resolve<StageProviderService>();

            RestState restState = CreateRestState();
            WaveProcessState waveProcessState = CreateWaveProcessState();

            ICompositeCondition fromRestToWave = new CompositeCondition()
                .Add(new FuncCondition(() => restCycleService.IsResting.Value))
                .Add(new FuncCondition(() => restCycleService.IsOver))
                .Add(new FuncCondition(() => stageProviderService.HasNextStage()));

            FuncCondition waveToRestCondition = new FuncCondition(() =>
                stageProviderService.CurrentStageResult.Value == StageResults.Completed
                && stageProviderService.HasNextStage());

            GameplayStateMachine coreLoopState = new GameplayStateMachine();

            coreLoopState.AddState(restState);
            coreLoopState.AddState(waveProcessState);

            coreLoopState.AddTransition(restState, waveProcessState, fromRestToWave);
            coreLoopState.AddTransition(waveProcessState, restState, waveToRestCondition);

            return coreLoopState;
        }
    }
}
