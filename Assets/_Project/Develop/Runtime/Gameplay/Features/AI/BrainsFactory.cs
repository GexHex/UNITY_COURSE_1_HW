using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Gameplay.Features.AI.States;
using Assets._Project.Develop.Runtime.Infrastructure.DI;
using Assets._Project.Develop.Runtime.Utilities.Reactive;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.AI
{
    public class BrainsFactory
    {
        private readonly AIBrainsContext _brainsContext;

        public BrainsFactory(DIContainer container)
        {
            _brainsContext = container.Resolve<AIBrainsContext>();
        }

        public StateMachineBrain CreateDummyEnemyBrain(Entity entity, Entity target)
        {
            entity.AddCurrentTarget(new ReactiveVariable<Entity>(target));

            AIStateMachine stateMachine = new AIStateMachine();
            stateMachine.AddState(new MoveToTargetState(entity));

            StateMachineBrain brain = new StateMachineBrain(stateMachine);
            _brainsContext.SetFor(entity, brain);

            return brain;
        }
    }
}
