using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Configs/Gameplay/LevelConfig", fileName = "LevelConfig")]
public class LevelConfig : ScriptableObject
{
    [field: SerializeField] public AgentEnemyConfig EnemyConfig { get; private set; }
    [field: SerializeField] public Vector3 MainHeroStartPosition { get; private set; }
    [field: SerializeField] public List<Vector3> EnemiesSpawnPoints { get; private set; }
    [field: SerializeField] public int TimeToEnemiesRespawn { get; private set; }
    [field: SerializeField] public string EnviromentSceneName { get; private set; } 

    [field: Header("Choose conditions")]
    [field: SerializeField] public ConditionTypesWin WinCondition { get; private set; }
    [field: SerializeField] public ConditionTypesDefeat DefeatCondition { get; private set; }

    [field: Header("Set win conditions parameters")]
    [field: SerializeField] public float TimeToWin { get; private set; } = 30;
    [field: SerializeField] public int EnemiesToKill { get; private set; } = 10;

    [field: Header("Set defeat conditions parameters")]
    [field: SerializeField] public int MaxEnemies { get; private set; } = 20;
    [field: SerializeField] public int MinMainHeroHealthToDeath { get; private set; } = 0;

    [ContextMenu("UpdateStartHeroPosition")]
    private void UpdateStartHeroPositon()
    {
        GameObject point = GameObject.FindGameObjectWithTag("StartHeroPosition");
        MainHeroStartPosition = point.transform.position;
    }

    [ContextMenu("UpdateStartEnemyPositions")]
    private void UpdateStartEnemiesPositon()
    {
        EnemiesSpawnPoints.Clear();

        GameObject[] points = GameObject.FindGameObjectsWithTag("StartEnemyPosition");

        foreach (GameObject point in points)
        {
            EnemiesSpawnPoints.Add(point.transform.position);
        }
    }
}