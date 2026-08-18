using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Gameplay.Features.AI.States;
using Assets._Project.Develop.Runtime.Gameplay.Features.InputFeature;
using Assets._Project.Develop.Runtime.Infrastructure.DI;
using Assets._Project.Develop.Runtime.Utilities.Conditions;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using Assets._Project.Develop.Runtime.Utilities.Timer;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.AI
{
    public class BrainsFactory
    {
        private readonly DIContainer _container;
        private readonly TimerServiceFactory _timerServiceFactory;
        private readonly AIBrainsContext _brainsContext;
        private readonly IInputService _inputService;
        private readonly EntitiesLifeContext _entitiesLifeContext;

        public BrainsFactory(DIContainer container)
        {
            _container = container;

            _timerServiceFactory = _container.Resolve<TimerServiceFactory>();
            _brainsContext = _container.Resolve<AIBrainsContext>();
            _inputService = _container.Resolve<IInputService>();
            _entitiesLifeContext = _container.Resolve<EntitiesLifeContext>();
        }

        public StateMachineBrain CreateMainHeroBrain(Entity entity)
        {
            PlayerInputMovementState movementState = new PlayerInputMovementState(entity, _inputService);
            PlayerMouseRotationState rotationState = new PlayerMouseRotationState(entity, _inputService);
            PlayerManualAttackState attackState = new PlayerManualAttackState(entity);

            ICondition canStartAttack = entity.CanStartAttack;

            ReactiveVariable<bool> inAttackProcess = entity.InAttackProcess;

            ICondition fromMovementToRotationStateCondition = new FuncCondition(() => _inputService.Direction == Vector3.zero); 
            ICondition fromRotationToMovementStateCondition = new FuncCondition(() => _inputService.Direction != Vector3.zero);

            ICompositeCondition fromRotationToAttackStateCondition = new CompositeCondition()
                .Add(canStartAttack)
                .Add(new FuncCondition(() => _inputService.Direction == Vector3.zero))
                .Add(new FuncCondition(() => _inputService.AttackPressed));

            ICondition fromAttackToMovementStateCondition = new FuncCondition(() =>
                _inputService.Direction != Vector3.zero);

            ICompositeCondition fromAttackToRotationStateCondition = new CompositeCondition()
                .Add(new FuncCondition(() => inAttackProcess.Value == false))
                .Add(new FuncCondition(() => _inputService.Direction == Vector3.zero));

            AIStateMachine stateMachine = new AIStateMachine();

            stateMachine.AddState(movementState);
            stateMachine.AddState(rotationState);
            stateMachine.AddState(attackState);

            stateMachine.AddTransition(movementState, rotationState, fromMovementToRotationStateCondition);
            stateMachine.AddTransition(rotationState, movementState, fromRotationToMovementStateCondition);
            stateMachine.AddTransition(rotationState, attackState, fromRotationToAttackStateCondition);
            stateMachine.AddTransition(attackState, movementState, fromAttackToMovementStateCondition);
            stateMachine.AddTransition(attackState, rotationState, fromAttackToRotationStateCondition);

            StateMachineBrain brain = new StateMachineBrain(stateMachine);

            _brainsContext.SetFor(entity, brain);

            return brain;
        }

        public StateMachineBrain CreateRandomTeleportBrain(Entity entity)
        {
            RandomTeleportState randomTeleportState = new RandomTeleportState(entity);

            AIStateMachine stateMachine = new AIStateMachine();
            stateMachine.AddState(randomTeleportState);

            StateMachineBrain brain = new StateMachineBrain(stateMachine);

            _brainsContext.SetFor(entity, brain);

            return brain;
        }

        public StateMachineBrain CreateIntelligentTeleportBrain(Entity entity)
        {
            LowestHealthDamageableTargetSelector targetSelector = new LowestHealthDamageableTargetSelector(entity);

            IntelligentTeleportState intelligentTeleportState = new IntelligentTeleportState(entity, _entitiesLifeContext, targetSelector, 0.4f);

            AIStateMachine stateMachine = new AIStateMachine();
            stateMachine.AddState(intelligentTeleportState);

            StateMachineBrain brain = new StateMachineBrain(stateMachine);

            _brainsContext.SetFor(entity, brain);

            return brain;
        }

        public StateMachineBrain CreateGhostBrain(Entity entity)
        {
            AIStateMachine stateMachine = CreateRandomMovementStateMachine(entity);
            StateMachineBrain brain = new StateMachineBrain(stateMachine);

            _brainsContext.SetFor(entity, brain);

            return brain;
        }

        private AIStateMachine CreateRandomMovementStateMachine(Entity entity)
        {
            List<IDisposable> disposables = new List<IDisposable>();

            RandomMovementState randomMovementState = new RandomMovementState(entity, 0.5f);

            EmptyState emptyState = new EmptyState();

            TimerService movementTimer = _timerServiceFactory.Create(2f);

            disposables.Add(movementTimer);
            disposables.Add(randomMovementState.Entered.Subscribe(movementTimer.Restart));

            TimerService idleTimer = _timerServiceFactory.Create(3f);

            disposables.Add(idleTimer);
            disposables.Add(emptyState.Entered.Subscribe(idleTimer.Restart));

            FuncCondition movementTimerEndedCondition = new FuncCondition(() => movementTimer.IsOver);
            FuncCondition idleTimerEndedCondition = new FuncCondition(() => idleTimer.IsOver);

            AIStateMachine stateMachine = new AIStateMachine(disposables);

            stateMachine.AddState(randomMovementState);
            stateMachine.AddState(emptyState);

            stateMachine.AddTransition(randomMovementState, emptyState, movementTimerEndedCondition);
            stateMachine.AddTransition(emptyState, randomMovementState, idleTimerEndedCondition);

            return stateMachine;
        }
    }
}
