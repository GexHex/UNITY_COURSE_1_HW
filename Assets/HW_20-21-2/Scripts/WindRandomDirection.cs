using UnityEngine;

namespace GameShip
{
    public class WindRandomDirection : MonoBehaviour
    {        
        [SerializeField] private float _minRandomeAngle = -180f;
        [SerializeField] private float _maxRandomeAngle = 180f;
        [SerializeField] private float _timeToChangeWindDirection = 3f;       
        [SerializeField] private float _rotateSpeed = 100f;
        private float _time;        
        private Quaternion _windRandomDirection;
        public Quaternion WindRotation { get; private set; }

        private void Awake()
        {
            SwitchDirection();
        }

        private void Update()
        {
            _time += Time.deltaTime;

            if (_time >= _timeToChangeWindDirection)
            {
                SwitchDirection();
                _time = 0;
            }

            WindRotation = Quaternion.RotateTowards(WindRotation, _windRandomDirection, _rotateSpeed * Time.deltaTime);     
        }

        private void SwitchDirection()
        {
            _windRandomDirection = Quaternion.Euler(0f, Random.Range(_minRandomeAngle, _maxRandomeAngle), 0f);
        }
    }
}