using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Utilities;
using Assets._Project.Develop.Runtime.Utilities.Conditions;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.AreaDamage
{
    public class AreaDamageValue : IEntityComponent
    {
        public ReactiveVariable<float> Value;
    }

    public class AreaDamageRadius : IEntityComponent
    {
        public ReactiveVariable<float> Value;
    }

    public class AreaDamageTargetsMask : IEntityComponent
    {
        public LayerMask Value;
    }

    public class AreaDamageCollidersBuffer : IEntityComponent
    {
        public Buffer<Collider> Value;
    }

    public class RunAreaDamageEvent : IEntityComponent
    {
        public ReactiveEvent Value;
    }

    public class CanRunAreaDamage : IEntityComponent
    {
        public ICompositeCondition Value;
    }

    public class AreaDamageTargetsBuffer : IEntityComponent
    {
        public Buffer<Entity> Value;
    }
}
