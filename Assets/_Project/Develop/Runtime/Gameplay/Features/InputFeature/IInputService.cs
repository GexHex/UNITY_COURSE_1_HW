using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.InputFeature
{
    public interface IInputService
    {
        bool IsEnabled { get; set; }

        bool IsClickDown { get; }

        bool TryGetClickWorldPosition(out Vector3 worldPosition);

        bool TryGetPointerPosition(out Vector2 screenPosition, out Vector3 gamePosition);
    }
}
