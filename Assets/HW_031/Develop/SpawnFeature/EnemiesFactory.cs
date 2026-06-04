using UnityEngine;

public class EnemiesFactory
{
    private ControllersUpdateService controllersUpdateService;
    private ControllersFactory controllerFactory;
    private CharactersFactory charactersFactory;

    public EnemiesFactory(
        ControllersUpdateService controllersUpdateService,
        ControllersFactory controllerFactory,
        CharactersFactory charactersFactory)
    {
        this.controllersUpdateService = controllersUpdateService;
        this.controllerFactory = controllerFactory;
        this.charactersFactory = charactersFactory;
    }

    public CharacterAgent CreateAgentEnemy(AgentEnemyConfig config, Vector3 spawnPosition, Transform target)
    {
        CharacterAgent instance = charactersFactory.CreateAgentCharacter(
             config.Prefab,
             spawnPosition,
             config.MoveSpeed,
             config.RotationSpeed,
             config.JumpSpeed,
             config.JumpCurve,
             config.TimeToSpawn,
             config.Health);

        Controller controller = controllerFactory.CreateAgentCharacterRandomPointsController(instance, config.AgroRange, 5);

        controller.Enable();

        controllersUpdateService.Add(controller, () => instance.IsDestroyed);

        return instance;
    }
}