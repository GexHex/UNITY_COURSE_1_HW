using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameMode
{
    public event Action Win;
    public event Action Defeat;

    private readonly LevelConfig _levelConfig;
    private readonly Character _mainHero;
    private readonly EnemiesSpawner _enemiesSpawner;

    private readonly ReactiveList<CharacterAgent> _spawnedEnemies = new();

    private ICondition _conditionWin;
    private ICondition _conditionDefeat;

    private bool _isRunning;
    private float _enemyRespawnTime;

    public GameMode(LevelConfig levelConfig, Character mainHero, EnemiesSpawner enemiesSpawner)
    {
        _levelConfig = levelConfig;
        _mainHero = mainHero;
        _enemiesSpawner = enemiesSpawner;

        CreateConditions();
    }

    private void CreateConditions()
    {
        switch (_levelConfig.WinCondition)
        {
            case ConditionTypesWin.SurviveTime:
                _conditionWin = new ConditionSurviveTime(_levelConfig.TimeToWin); break;

            case ConditionTypesWin.KillEnemies:
                _conditionWin = new ConditionKillEnemies(_spawnedEnemies, _levelConfig.EnemiesToKill); break;
        }

        switch (_levelConfig.DefeatCondition)
        {
            case ConditionTypesDefeat.MainHeroDeath:
                _conditionDefeat = new ConditionMainHeroDeath(_mainHero, _levelConfig.MinMainHeroHealthToDeath); break;

            case ConditionTypesDefeat.TooManyEnemies:
                _conditionDefeat = new ConditionTooManyEnemies(_spawnedEnemies, _levelConfig.MaxEnemies); break;
        }

        _conditionWin.Completed += ProcessWin;
        _conditionDefeat.Completed += ProcessDefeat;
    }

    public void Start()
    {
        _enemyRespawnTime = _levelConfig.TimeToEnemiesRespawn;

        SpawnEnemies();

        _isRunning = true;
    }

    public void Update(float deltaTime)
    {
        if (_isRunning == false)
            return;

        ProcessEnemyWavesSpawning(deltaTime);

        _conditionWin.Update(deltaTime);
        _conditionDefeat.Update(deltaTime);
    }

    private void ProcessEnemyWavesSpawning(float deltaTime)
    {
        _enemyRespawnTime -= deltaTime;

        if (_enemyRespawnTime > 0)
            return;

        SpawnEnemies();

        _enemyRespawnTime = Mathf.Max(0.1f, _levelConfig.TimeToEnemiesRespawn);
    }

    private void SpawnEnemies()
    {
        List<CharacterAgent> enemies = _enemiesSpawner.Spawn(_levelConfig.EnemyConfig, _mainHero.transform, _levelConfig.EnemiesSpawnPoints);

        foreach (CharacterAgent enemy in enemies)
            RegisterEnemy(enemy);
    }

    private void RegisterEnemy(CharacterAgent enemy)
    {
        _spawnedEnemies.Add(enemy);

        enemy.Died += OnEnemyDied;
    }

    private void UnregisterEnemy(CharacterAgent enemy)
    {
        enemy.Died -= OnEnemyDied;

        _spawnedEnemies.Remove(enemy);
    }

    private void OnEnemyDied(CharacterAgent enemy)
    {
        UnregisterEnemy(enemy);

        enemy.DestroySelf();
    }

    private void ProcessEndGame()
    {
        _isRunning = false;

        _conditionWin.Completed -= ProcessWin;
        _conditionDefeat.Completed -= ProcessDefeat;

        foreach (CharacterAgent enemy in _spawnedEnemies.Items.ToArray())
        {
            UnregisterEnemy(enemy);

            enemy.DestroySelf();
        }
    }

    private void ProcessWin()
    {
        ProcessEndGame();

        Win?.Invoke();
    }

    private void ProcessDefeat()
    {
        ProcessEndGame();

        Defeat?.Invoke();
    }
}