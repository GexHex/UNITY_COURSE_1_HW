using UnityEngine;

public class ControllersFactory : MonoBehaviour
{
    public CompositeController CreateMainHeroPlayerController(Character character)
    {
        return new CompositeController(
            CreatePlayerDirectionalMovableController(character),
            CreateAlongMovableVelocityRotatebleController(character, character));
    }

    public PlayerDirectionalMovableController CreatePlayerDirectionalMovableController(IDirectionalMovable movable)
    {
        return new PlayerDirectionalMovableController(movable);
    }

    public AlongMovableVelocityRotatebleController CreateAlongMovableVelocityRotatebleController(
        IDirectionalMovable movable,
        IDirectionalRotatable rotatable)
    {
        return new AlongMovableVelocityRotatebleController(rotatable, movable);
    }

    public ControllerRandomPoints CreateControllerRandomPoints(IDirectionalMovable movable, IDirectionalRotatable rotator)
    {
        return new ControllerRandomPoints(movable, rotator);
    }

    public AgentCharacterRandomPointsController CreateAgentCharacterRandomPointsController(
        CharacterAgent character,
        float radius = 5f,
        int checkpointsCount = 5)
    {
        return new AgentCharacterRandomPointsController(
            character,
            radius,
            checkpointsCount);
    }
}