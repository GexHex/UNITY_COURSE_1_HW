using UnityEngine;

namespace GameShip
{
    public class WindView : MonoBehaviour
    {
        [SerializeField] private Transform _deck;
        [SerializeField] private Transform _camera;
        [SerializeField] private WindRandomDirection _wind;
        [SerializeField] private Transform _windView;
        private Vector3 _startPosition;
        private Quaternion _startRotation;

        private void Awake()
        {
            _startPosition = transform.position;
            _startRotation = transform.rotation;
        }

        private void Update()
        {
            transform.position = _startPosition + _deck.position;
            transform.rotation = _startRotation * _camera.rotation;
            _windView.rotation = _wind.WindRotation;
        }
    }
}