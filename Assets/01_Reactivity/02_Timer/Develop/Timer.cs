using System;

namespace Timer
{
    public class Timer
    {
        public event Action Started;
        public event Action Stopped;
        public event Action Reset;
        public event Action TimeIsUp;

        private readonly float _minTime = 0;
        private bool _isWorking;

        private ReactiveVariable<float> _currentTime;

        public IReadOnlyVariable<float> CurrentTime => _currentTime;

        public Timer()
        {
            _currentTime = new ReactiveVariable<float>();
        }

        public void Update(float deltaTime)
        {
            if (_isWorking == false)
                return;

            UpdateTime(deltaTime);
        }

        public void Start()
        {
            if (_isWorking)
                return;

            _isWorking = true;

            Started?.Invoke();
        }

        public void Stop()
        {
            if (_isWorking == false)
                return;

            _isWorking = false;

            Stopped?.Invoke();
        }

        public void ResetTime(float time)
        {
            _isWorking = false;

            _currentTime.Value = time;

            Reset?.Invoke();
        }

        private void UpdateTime(float deltaTime)
        {
            if (CurrentTime.Value <= 0)
            {
                _currentTime.Value = _minTime;

                _isWorking = false;

                TimeIsUp?.Invoke();

                return;
            }

            _currentTime.Value -= deltaTime;
        }
    }
}