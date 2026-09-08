using Assets._Project.Develop.Runtime.Utilities;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.InputFeature
{
    public class DesktopInput : IInputService
    {
        private Vector2 _lastPointerScreenPosition;
        private bool _hasLastPointerScreenPosition;

        public bool IsEnabled { get; set; } = true;

        public bool IsClickDown
        {
            get
            {
                if (IsEnabled == false)
                    return false;

                if (Input.GetMouseButtonDown(0) == false)
                    return false;

                if (IsPointerOverButton())
                    return false;

                return true;
            }
        }

        public bool TryGetClickWorldPosition(out Vector3 worldPosition) => TryProjectPointerToWorldPosition(out worldPosition);

        public bool TryGetPointerPosition(out Vector2 screenPosition, out Vector3 gamePosition)
        {
            screenPosition = Input.mousePosition;
            gamePosition = Vector3.zero;

            return TryProjectPointerToWorldPosition(out gamePosition);
        }

        private bool TryProjectPointerToWorldPosition(out Vector3 worldPosition)
        {
            worldPosition = Vector3.zero;

            if (IsEnabled == false)
                return false;

            if (Camera.main == null)
                return false;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, 500f, Layers.EnviromentMask, QueryTriggerInteraction.Ignore))
            {
                worldPosition = hit.point;
                worldPosition.y = 0f;

                return true;
            }

            Plane ground = new Plane(Vector3.up, Vector3.zero);

            if (ground.Raycast(ray, out float enter))
            {
                worldPosition = ray.GetPoint(enter);
                worldPosition.y = 0f;

                return true;
            }

            return false;
        }

        private static bool IsPointerOverButton()
        {
            if (EventSystem.current == null)
                return false;

            PointerEventData eventData = new PointerEventData(EventSystem.current)
            {
                position = Input.mousePosition
            };

            List<RaycastResult> results = new List<RaycastResult>();

            EventSystem.current.RaycastAll(eventData, results);

            for (int i = 0; i < results.Count; i++)
            {
                if (results[i].gameObject.GetComponentInParent<Selectable>() != null)
                    return true;
            }

            return false;
        }
    }
}
