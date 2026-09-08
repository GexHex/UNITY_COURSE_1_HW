using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore.Systems;
using Assets._Project.Develop.Runtime.Utilities.Conditions;
using Assets._Project.Develop.Runtime.Utilities.Reactive;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.Attack
{
    public class StartAttackOnTouchSystem : IInitializableSystem, IUpdatableSystem
    {
        private ReactiveEvent _startAttackRequest;
        private ReactiveVariable<bool> _inAttackProcess;
        private ICompositeCondition _canStartAttack;

        public void OnInit(Entity entity)
        {
            _startAttackRequest = entity.StartAttackRequest;
            _inAttackProcess = entity.InAttackProcess;
            _canStartAttack = entity.CanStartAttack;
        }

        public void OnUpdate(float deltaTime)
        {
            if (_inAttackProcess.Value)
                return;

            if (_canStartAttack.Evaluate() == false)
                return;

            _startAttackRequest.Invoke();
        }
    }
}
