using System;

namespace Timer
{
    public class Timer
    {
        public event Action Started;
        public event Action Stopped;
        public event Action<float> Reset;
        public event Action<float> Changed;
        public event Action TimeIsUp;

        private bool _isTimerWork;
        private float _currentTime;
        private float _minTime = 0;

        public float CurrentTime => _currentTime;

        public void Update(float deltaTime)
        {
            if (_isTimerWork)
                UpdateTime(deltaTime);
        }

        public void Start()
        {
            _isTimerWork = true;

            Started?.Invoke();
        }

        public void Stop()
        {
            _isTimerWork = false;

            Stopped?.Invoke();
        }

        public void ResetTime(float time)
        {
            _isTimerWork = false;
            _currentTime = time;

            Reset?.Invoke(_currentTime);
        }

        private void UpdateTime(float deltaTime)
        {
            if (_currentTime <= 0)
            {
                _currentTime = _minTime;
                _isTimerWork = false;

                TimeIsUp?.Invoke();
            }
            else
            {
                _currentTime -= deltaTime;

                Changed?.Invoke(_currentTime);
            }
        }
    }
}