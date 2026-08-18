using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Utilities.Conditions;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using Assets._Project.Develop.Runtime.Utilities.StateMachineCore;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.AI.States
{   
    public class RandomTeleportState : State, IUpdatableState
    {
        private readonly Transform _transform;
        private readonly ReactiveEvent<Vector3> _teleportRequest;
        private readonly ReactiveVariable<float> _interval;
        private readonly ReactiveVariable<float> _radius;
        private readonly ICompositeCondition _canTeleport;

        private float _elapsedTime;
        private bool _waitingForEnergy;

        public RandomTeleportState(Entity entity)
        {
            _transform = entity.Transform;
            _teleportRequest = entity.RunTeleportEvent;
            _interval = entity.TeleportInterval;
            _radius = entity.TeleportRadius;
            _canTeleport = entity.CanTeleport;
        }

        public override void Enter()
        {
            base.Enter();

            _elapsedTime = 0f;
            _waitingForEnergy = false;
        }

        public void Update(float deltaTime)
        {
            if (_waitingForEnergy == false)
            {
                _elapsedTime += deltaTime;

                if (_elapsedTime < Mathf.Max(0.01f, _interval.Value))
                    return;

                _waitingForEnergy = true;
            }

            if (_canTeleport.Evaluate() == false)
                return;

            Vector2 randomOffset = Random.insideUnitCircle * Mathf.Max(0f, _radius.Value);
            Vector3 destination = _transform.position + new Vector3(randomOffset.x, 0f, randomOffset.y);

            _teleportRequest.Invoke(destination);
            _elapsedTime = 0f;
            _waitingForEnergy = false;
        }
    }
}
