using UnityEngine;

namespace GameShip
{
    public class Ship : MonoBehaviour
    {
        [SerializeField] private Transform _sail;
        [SerializeField] private Transform _deck;
        [SerializeField] private WindRandomDirection _randomWindDirection;
        [SerializeField] private float _minSailAngle = -90f;
        [SerializeField] private float _maxSailAngle = 90f;
        [SerializeField] private float _deckRotationSpeed = 60;
        [SerializeField] private float _sailRotationSpeed = 60;
        [SerializeField] private GameShip.UserInput _userInput;
        private float _dotProductWindAndSail;
        private float _dotProductDeckAndSail;
        private float _sailCurrentAngle;
        private Vector3 _sailCurrentDirection;
        private Vector3 _deckCurrentDirection;
        private Vector3 _windCurrentDirection;

        private void Update()
        {
            TramsformSail();
            TransformDeck();
        }

        private void TramsformSail()
        {
            _sailCurrentAngle = _sail.transform.localEulerAngles.y;

            RotateSail();

            if (_sailCurrentAngle > 180)
                _sailCurrentAngle -= 360;

            _sailCurrentAngle = Mathf.Clamp(_sailCurrentAngle, _minSailAngle, _maxSailAngle);
            _sail.transform.localRotation = Quaternion.Euler(0f, _sailCurrentAngle, 0f);
        }

        private void TransformDeck()
        {
            _sailCurrentDirection = _sail.transform.forward;
            _deckCurrentDirection = _deck.transform.forward;
            _windCurrentDirection = _randomWindDirection.WindRotation * Vector3.forward;           

            _dotProductDeckAndSail = Vector3.Dot(_sailCurrentDirection, _deckCurrentDirection);
            _dotProductWindAndSail = Vector3.Dot(_sailCurrentDirection, _windCurrentDirection);           

            if (_dotProductWindAndSail > 0 && _dotProductDeckAndSail > 0)
            {
                _deck.transform.Translate(Vector3.forward * _dotProductWindAndSail * _dotProductDeckAndSail * Time.deltaTime, Space.Self);

                RotateDeck();
            }

            Debug.Log($"Ветер: {_windCurrentDirection} Парус: {_sailCurrentDirection} Палуба: {_deckCurrentDirection}");
        }

        private void RotateSail()
        {
            if (_userInput.RotateSailLeft)
                _sailCurrentAngle += _sailRotationSpeed * Time.deltaTime;
            if (_userInput.RotateSailRight)
                _sailCurrentAngle -= _sailRotationSpeed * Time.deltaTime;
        }

        private void RotateDeck()
        {
            if (_userInput.RotateDeckLeft)
                _deck.transform.Rotate(Vector3.up * _deckRotationSpeed * _dotProductWindAndSail * Time.deltaTime);
            if (_userInput.RotateDeckRight)
                _deck.transform.Rotate(Vector3.up * -_deckRotationSpeed * _dotProductWindAndSail * Time.deltaTime);
        }
    }
}