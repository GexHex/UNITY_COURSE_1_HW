using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Utilities.Reactive;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.Explosions
{
    public class ExplosionRadius : IEntityComponent
    {
        public ReactiveVariable<float> Value;
    }

    public class ExplosionDamage : IEntityComponent
    {
        public ReactiveVariable<float> Value;
    }
}
