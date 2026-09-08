using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.PlayerInteraction
{
    public class PlayerExplosionEvent : IEntityComponent
    {
        public ReactiveEvent<Vector3> Value;
    }
}
