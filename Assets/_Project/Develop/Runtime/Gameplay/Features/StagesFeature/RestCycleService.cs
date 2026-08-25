using Assets._Project.Develop.Runtime.Utilities.Reactive;
using Assets._Project.Develop.Runtime.Utilities.Timer;
using System;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.StagesFeature
{
    public class RestCycleService : IDisposable
    {
        private readonly TimerServiceFactory _timerServiceFactory;
        private readonly ReactiveVariable<bool> _isResting = new();
        private readonly ReactiveVariable<float> _remainingTime = new();

        private TimerService _timer;
        private IDisposable _timerDisposable;

        public RestCycleService(TimerServiceFactory timerServiceFactory)
        {
            _timerServiceFactory = timerServiceFactory;
        }

        public IReadOnlyVariable<bool> IsResting => _isResting;

        public IReadOnlyVariable<float> RemainingTime => _remainingTime;

        public bool IsOver => _isResting.Value == false || _remainingTime.Value <= 0;

        public void Begin(float duration)
        {
            CleanupTimer();

            _timer = _timerServiceFactory.Create(duration);
            _remainingTime.Value = duration;
            _isResting.Value = true;

            _timerDisposable = _timer.CurrentTime.Subscribe(OnTimerChanged);
            _timer.Restart();
        }

        public void Cleanup()
        {
            CleanupTimer();
            _isResting.Value = false;
            _remainingTime.Value = 0;
        }

        public void Dispose()
        {
            Cleanup();
        }

        private void OnTimerChanged(float oldValue, float newValue)
        {
            _remainingTime.Value = newValue;
        }

        private void CleanupTimer()
        {
            _timerDisposable?.Dispose();
            _timerDisposable = null;
            _timer?.Dispose();
            _timer = null;
        }
    }
}
