using Assets._Project.Develop.Runtime.Configs.Gameplay.Stages;
using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Gameplay.Features.BuildingFeature;
using Assets._Project.Develop.Runtime.Gameplay.Features.Enemies;
using Assets._Project.Develop.Runtime.Infrastructure.DI;
using System;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.StagesFeature
{
    public class StagesFactory
    {
        private readonly DIContainer _container;

        public StagesFactory(DIContainer container)
        {
            _container = container;
        }

        public IStage Create(StageConfig stageConfig)
        {
            switch (stageConfig)
            {
                case WaveStageConfig waveStageConfig:
                    return new WaveStage(
                        waveStageConfig,
                        _container.Resolve<EnemiesFactory>(),
                        _container.Resolve<EntitiesLifeContext>(),
                        _container.Resolve<BuildingHolderService>());

                default:
                    throw new ArgumentException($"Not supported {stageConfig.GetType()} type config");
            }
        }
    }
}
