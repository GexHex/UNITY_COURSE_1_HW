using System;
using UnityEngine;

namespace DestroyService
{
    public class InputUser : MonoBehaviour
    {
        public Action EntityCreated01;
        public Action EntityCreated02;
        public Action EntityCreated03;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
                EntityCreated01?.Invoke();

            if (Input.GetKeyDown(KeyCode.Alpha2))
                EntityCreated02?.Invoke();

            if (Input.GetKeyDown(KeyCode.Alpha3))
                EntityCreated03?.Invoke();
        }
    }
}