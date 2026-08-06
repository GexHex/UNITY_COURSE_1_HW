using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Utilities.Conditions;
using Assets._Project.Develop.Runtime.Utilities.Reactive;

namespace Assets._Project.Develop.Runtime.Gameplay.Features._test
{
    public class EnergyComponent : IEntityComponent
    {
        public ReactiveVariable<int> Value;
    }

    public class RunTeleportEvent : IEntityComponent
    {
        public ReactiveEvent Value;
    }

    public class StatsTimerEvent : IEntityComponent
    {
        public ReactiveEvent Value;
    }

    public class CanTeleport : IEntityComponent
    {
        public ICompositeCondition Value;
    }
}
