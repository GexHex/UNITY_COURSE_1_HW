using UnityEngine;
using UnityEngine.AI;

public class MoverService
{
    private NavMeshAgent _navMeshAgent;

    public Vector3 CurrentVelosity => _navMeshAgent.desiredVelocity;

    public MoverService(NavMeshAgent agent, float movementSpeed)
    {
        _navMeshAgent = agent;
        _navMeshAgent.speed = movementSpeed;
        _navMeshAgent.acceleration = 999;
    }

    public void SetMoveDirection(Vector3 position)
    {
        _navMeshAgent.SetDestination(position);
    }
}