using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Utilities.Conditions;
using Assets._Project.Develop.Runtime.Utilities.Reactive;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.Teleport
{  
    public class RunTeleportEvent : IEntityComponent
    {
        public ReactiveEvent Value;
    }

    public class CompletedTeleportEvent : IEntityComponent
    {
        public ReactiveEvent Value;    
    }   

    public class CanTeleport : IEntityComponent
    {
        public ICompositeCondition Value;
    }
}