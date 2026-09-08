using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.PlayerInteraction
{
    public struct PointerMoveArgs
    {
        public Vector2 ScreenPosition;
        public Vector3 GamePosition;

        public PointerMoveArgs(Vector2 screenPosition, Vector3 gamePosition)
        {
            ScreenPosition = screenPosition;
            GamePosition = gamePosition;
        }
    }

    public class PlayerPointerMoveEvent : IEntityComponent
    {
        public ReactiveEvent<PointerMoveArgs> Value;
    }
}
