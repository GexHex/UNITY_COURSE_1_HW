using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Utilities.Reactive;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.RotationFeature
{
    public class RotationDirection : IEntityComponent
    {
        public ReactiveVariable<float> Value;
    }

    public class RotationSpeed : IEntityComponent
    {
        public ReactiveVariable<float> Value;
    }
}