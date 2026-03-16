using UnityEngine;

namespace GameShip
{
    public class UserInput : MonoBehaviour
    {
        public bool RotateSailLeft { get; private set; }
        public bool RotateSailRight { get; private set; }
        public bool RotateDeckLeft { get; private set; }
        public bool RotateDeckRight { get; private set; }

        private void Update()
        {
            RotateSailLeft = Input.GetKey(KeyCode.Q);
            RotateSailRight = Input.GetKey(KeyCode.W);
            RotateDeckLeft = Input.GetKey(KeyCode.A);
            RotateDeckRight = Input.GetKey(KeyCode.S);
        }
    }
}