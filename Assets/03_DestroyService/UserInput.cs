using System;
using UnityEngine;

namespace DestroyService
{
    public class UserInput : MonoBehaviour
    {
        public Action DragonCreated;
        public Action ElfCreated;
        public Action OgrCreated;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
                DragonCreated?.Invoke();

            if (Input.GetKeyDown(KeyCode.Alpha2))
                ElfCreated?.Invoke();

            if (Input.GetKeyDown(KeyCode.Alpha3))
                OgrCreated?.Invoke();
        }
    }
}