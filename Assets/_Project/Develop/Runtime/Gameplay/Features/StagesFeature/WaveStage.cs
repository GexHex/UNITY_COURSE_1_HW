using Assets._Project.Develop.Runtime.Configs.Gameplay.Stages;
using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Gameplay.Features.BuildingFeature;
using Assets._Project.Develop.Runtime.Gameplay.Features.Enemies;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.StagesFeature
{
    public class WaveStage : IStage
    {
        private WaveStageConfig _config;
        private ReactiveEvent _completed = new();
        private EnemiesFactory _enemiesFactory;
        private EntitiesLifeContext _entitiesLifeContext;
        private BuildingHolderService _buildingHolderService;

        private bool _inProcess;

        private Dictionary<Entity, IDisposable> _spawnedEnemiesToRemoveReason = new();

        public WaveStage(
            WaveStageConfig config,
            EnemiesFactory enemiesFactory,
            EntitiesLifeContext entitiesLifeContext,
            BuildingHolderService buildingHolderService)
        {
            _config = config;
            _enemiesFactory = enemiesFactory;
            _entitiesLifeContext = entitiesLifeContext;
            _buildingHolderService = buildingHolderService;
        }

        public IReadOnlyEvent Completed => _completed;

        public void Cleanup()
        {
            foreach (KeyValuePair<Entity, IDisposable> item in _spawnedEnemiesToRemoveReason)
            {
                item.Value.Dispose();
                _entitiesLifeContext.Release(item.Key);
            }

            _spawnedEnemiesToRemoveReason.Clear();

            _inProcess = false;
        }

        public void Dispose()
        {
            foreach (KeyValuePair<Entity, IDisposable> item in _spawnedEnemiesToRemoveReason)
            {
                item.Value.Dispose();
            }

            _spawnedEnemiesToRemoveReason.Clear();

            _inProcess = false;
        }

        public void Start()
        {
            if (_inProcess)
                throw new InvalidOperationException("Game mode уже стартанул!");

            SpawnEnemies();

            _inProcess = true;
        }

        public void Update(float deltaTime)
        {
            if (_inProcess == false)
                return;

            if (_spawnedEnemiesToRemoveReason.Count == 0)
                ProcessEnd();
        }

        private void ProcessEnd()
        {
            _inProcess = false;
            _completed.Invoke();
        }

        private void SpawnEnemies()
        {
            Vector3 towerPosition = Vector3.zero;

            if (_buildingHolderService.Building != null)
                towerPosition = _buildingHolderService.Building.Transform.position;

            for (int i = 0; i < _config.EnemiesCount; i++)
                SpawnEnemy(GetSpawnPosition(towerPosition, _config.SpawnDistance));
        }

        private void SpawnEnemy(Vector3 position)
        {
            Entity spawnedEnemy = _enemiesFactory.Create(position, _config.EnemyConfig);

            IDisposable removeReason = spawnedEnemy.InDeathProcess.Subscribe((oldValue, inDeathProcess) =>
            {
                if (spawnedEnemy.IsDead.Value && inDeathProcess == false)
                {
                    if (_spawnedEnemiesToRemoveReason.TryGetValue(spawnedEnemy, out IDisposable disposable))
                    {
                        disposable.Dispose();
                        _spawnedEnemiesToRemoveReason.Remove(spawnedEnemy);
                    }
                }
            });

            _spawnedEnemiesToRemoveReason.Add(spawnedEnemy, removeReason);
        }

        private Vector3 GetSpawnPosition(Vector3 center, float distance)
        {
            float angle = UnityEngine.Random.Range(0f, 360f) * Mathf.Deg2Rad;

            return center + new Vector3(Mathf.Cos(angle), 0f, Mathf.Sin(angle)) * distance;
        }
    }
}
