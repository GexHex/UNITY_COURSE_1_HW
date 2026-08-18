using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Utilities.Conditions;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.Teleport
{  
    public class RunTeleportEvent : IEntityComponent
    {
        public ReactiveEvent<Vector3> Value;
    }

    public class CompletedTeleportEvent : IEntityComponent
    {
        public ReactiveEvent Value;    
    }   

    public class CanTeleport : IEntityComponent
    {
        public ICompositeCondition Value;
    }

    public class TeleportRadius : IEntityComponent
    {
        public ReactiveVariable<float> Value;
    }

    public class TeleportInterval : IEntityComponent
    {
        public ReactiveVariable<float> Value;
    }

    public class TeleportEnergyCost : IEntityComponent
    {
        public ReactiveVariable<int> Value;
    }
}
