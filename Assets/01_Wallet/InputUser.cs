using System;
using UnityEngine;

namespace Wallet
{
    public class InputUser : MonoBehaviour
    {
        public Action AddItem01;
        public Action AddItem02;
        public Action AddItem03;
        public Action DeleteItem01;
        public Action DeleteItem02;
        public Action DeleteItem03;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
                AddItem01?.Invoke();

            if (Input.GetKeyDown(KeyCode.W))
                AddItem02?.Invoke();

            if (Input.GetKeyDown(KeyCode.E))
                AddItem03?.Invoke();

            if (Input.GetKeyDown(KeyCode.A))
                DeleteItem01?.Invoke();

            if (Input.GetKeyDown(KeyCode.S))
                DeleteItem02?.Invoke();

            if (Input.GetKeyDown(KeyCode.D))
                DeleteItem03?.Invoke();
        }
    }
}