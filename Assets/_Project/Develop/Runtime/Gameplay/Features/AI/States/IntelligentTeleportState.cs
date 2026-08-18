using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Utilities.Conditions;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using Assets._Project.Develop.Runtime.Utilities.StateMachineCore;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.AI.States
{    
    public class IntelligentTeleportState : State, IUpdatableState
    {
        private readonly EntitiesLifeContext _entitiesLifeContext;
        private readonly ITargetSelector _targetSelector;
        private readonly Transform _transform;
        private readonly ReactiveEvent<Vector3> _teleportRequest;
        private readonly ReactiveVariable<float> _interval;
        private readonly ReactiveVariable<float> _radius;
        private readonly ReactiveVariable<int> _energy;
        private readonly ReactiveVariable<int> _maxEnergy;
        private readonly ICompositeCondition _canTeleport;
        private readonly float _energyThreshold;
        private float _minDistanceToTarget = 2f;

        private float _elapsedTime;
        private bool _waitingForEnergy;

        public IntelligentTeleportState(
            Entity entity,
            EntitiesLifeContext entitiesLifeContext,
            ITargetSelector targetSelector,
            float energyThreshold = 0.4f)
        {
            _entitiesLifeContext = entitiesLifeContext;
            _targetSelector = targetSelector;
            _transform = entity.Transform;
            _teleportRequest = entity.RunTeleportEvent;
            _interval = entity.TeleportInterval;
            _radius = entity.TeleportRadius;
            _energy = entity.Energy;
            _maxEnergy = entity.MaxEnergy;
            _canTeleport = entity.CanTeleport;
            _energyThreshold = Mathf.Clamp01(energyThreshold);        
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

            int requiredEnergy = Mathf.CeilToInt(_maxEnergy.Value * _energyThreshold);

            if (_energy.Value < requiredEnergy)
                return;

            if (_canTeleport.Evaluate() == false)
                return;

            Entity target = _targetSelector.SelectTargetFrom(_entitiesLifeContext.Entities);

            if (target == null)
                return;

            Vector3 direction = target.Transform.position - _transform.position;
            direction.y = 0f;

            if (direction.sqrMagnitude <= 0.0001f)
                return;

            float distance = Mathf.Max(0f, direction.magnitude - _minDistanceToTarget);
            float step = Mathf.Min(distance, Mathf.Max(0f, _radius.Value));
            Vector3 destination = _transform.position + direction.normalized * step;

            _teleportRequest.Invoke(destination);
            _elapsedTime = 0f;
            _waitingForEnergy = false;
        }
    }
}
