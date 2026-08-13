using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Utilities.Reactive;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.Energy
{
    public class EnergyComponent : IEntityComponent
    {
        public ReactiveVariable<int> Value;
    }

    public class FullOfEnergyEvent : IEntityComponent
    {
        public ReactiveEvent Value;
    }
}
