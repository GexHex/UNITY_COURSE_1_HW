using System;
using UnityEngine;
using UnityEngine.AI;
using Object = UnityEngine.Object;

public class CharactersFactory
{
    public Character CreateCharacter(Character prefab, Vector3 spawnPosition, float moveSpeed, float rotationSpeed, int health)
    {
        Character instance = Object.Instantiate(prefab, spawnPosition, Quaternion.identity, null);       

        DirectionalMover mover;
        DirectionalRotator rotator;

        if (instance.TryGetComponent(out CharacterController characterController))
        {
            mover = new CharacterDirectionalMover(characterController, moveSpeed);
            rotator = new TransformDirectionalRotator(instance.transform, rotationSpeed);
        }
        else if (instance.TryGetComponent(out Rigidbody rigidbody))
        {
            mover = new RigidbodyDirectionalMover(rigidbody, moveSpeed);
            rotator = new RigidbodyDirectionalRotator(rigidbody, rotationSpeed);
        }
        else
        {
            throw new InvalidOperationException("Not found mover component");
        }

        instance.Initialize(mover, rotator, health);

        return instance;
    }

    public CharacterAgent CreateAgentCharacter(
        CharacterAgent prefab,
        Vector3 spawnPosition,
        float moveSpeed,
        float rotationSpeed,
        float jumpSpeed,
        AnimationCurve jumpCurve,
        float timeToSpawn,
        int health)
    {
        CharacterAgent instance = Object.Instantiate(prefab, spawnPosition, Quaternion.identity, null);

        NavMeshAgent agent;

        if (instance.TryGetComponent(out agent) == false)
            throw new InvalidOperationException("Not found mover component");

        agent.updateRotation = false;  //!!!

        AgentMover mover = new AgentMover(agent, moveSpeed);

        TransformDirectionalRotator rotator = new TransformDirectionalRotator(instance.transform, rotationSpeed);

        AgentJumper jumper = new AgentJumper(jumpSpeed, agent, instance, jumpCurve);

        Timer spawnTimer = new Timer(instance);

        instance.Initialize(agent, mover, rotator, jumper, spawnTimer, timeToSpawn, health);

        return instance;
    }
}