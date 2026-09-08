using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore.Systems;
using Assets._Project.Develop.Runtime.Utilities;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.Sensors
{
    public class SphereContactsDetectingSystem : IInitializableSystem, IUpdatableSystem
    {
        private Buffer<Collider> _contacts;
        private LayerMask _mask;

        private SphereCollider _body;

        public void OnInit(Entity entity)
        {
            _contacts = entity.ContactCollidersBuffer;
            _mask = entity.ContactsDetectingMask;

            _body = entity.SphereBodyCollider;
        }

        public void OnUpdate(float deltaTime)
        {
            float radius = _body.radius * Mathf.Max(
                _body.transform.lossyScale.x,
                _body.transform.lossyScale.y,
                _body.transform.lossyScale.z);

            _contacts.Count = Physics.OverlapSphereNonAlloc(
                _body.bounds.center,
                radius,
                _contacts.Items,
                _mask,
                QueryTriggerInteraction.Collide);

            RemoveSelfFromContacts();
        }

        private void RemoveSelfFromContacts()
        {
            int indexToRemove = -1;

            for (int i = 0; i < _contacts.Count; i++)
            {
                if (_contacts.Items[i] == _body)
                {
                    indexToRemove = i;
                    break;
                }
            }

            if (indexToRemove >= 0)
            {
                for (int i = indexToRemove; i < _contacts.Count - 1; i++)
                {
                    _contacts.Items[i] = _contacts.Items[i + 1];
                }

                _contacts.Count--;
            }
        }
    }
}
