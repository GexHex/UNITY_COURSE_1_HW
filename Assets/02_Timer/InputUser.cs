using System;
using UnityEngine;

namespace Timer
{
    public class InputUser : MonoBehaviour
    {
        public Action StartPressed;
        public Action StopPressed;
        public Action ResetPressed;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
                StartPressed?.Invoke();

            if (Input.GetKeyDown(KeyCode.W))
                StopPressed?.Invoke();

            if (Input.GetKeyDown(KeyCode.E))
                ResetPressed?.Invoke();
        }
    }
}