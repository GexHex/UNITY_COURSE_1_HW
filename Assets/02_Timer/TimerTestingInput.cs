using System;
using UnityEngine;

namespace Timer
{
    public class TimerTestingInput : MonoBehaviour
    {
        public event Action StartPressed;
        public event Action StopPressed;
        public event Action ResetPressed;

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