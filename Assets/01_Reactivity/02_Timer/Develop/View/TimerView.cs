using UnityEngine;

namespace Timer
{
    public class TimerView : MonoBehaviour
    {
        private const string StartText = "Start";
        private const string StopText = "Stop";
        private const string ResetText = "Stop (Reset)";
        private const string TimeIsUpText = "Time is Up!";

        [SerializeField] private TimerSliderView _slider;
        [SerializeField] private TimerHeartsView _hearts;
        [SerializeField] private TimerTextView _text;

        private Timer _timer;

        public void Initialize(Timer timer)
        {
            _timer = timer;

            _timer.Started += OnStarted;
            _timer.Stopped += OnStopped;
            _timer.Reset += OnReset;
            _timer.TimeIsUp += OnTimeIsUp;

            _timer.CurrentTime.Changed += OnChanged;

            ForceInit();
        }

        private void OnDestroy()
        {
            if (_timer == null)
                return;

            _timer.Started -= OnStarted;
            _timer.Stopped -= OnStopped;
            _timer.Reset -= OnReset;
            _timer.TimeIsUp -= OnTimeIsUp;

            _timer.CurrentTime.Changed -= OnChanged;
        }

        private void ForceInit()
        {
            float time = _timer.CurrentTime.Value;

            _text.SetTime(time);

            _slider.SetValue(time);

            _hearts.Render(time);
        }

        private void OnStarted()
        {
            _text.SetInfo(StartText);
        }

        private void OnStopped()
        {
            if (_text.CurrentInfo == ResetText)
                return;

            if (_text.CurrentInfo == TimeIsUpText)
                return;

            _text.SetInfo(StopText);
        }

        private void OnReset()
        {
            float time = _timer.CurrentTime.Value;

            _text.SetInfo(ResetText);
            _text.SetTime(time);

            _slider.SetValue(time);
            
            _hearts.Clear();
            _hearts.Render(time);
        }

        private void OnChanged(float oldValue, float newValue)
        {
            _text.SetTime(newValue);

            _slider.SetValue(newValue);

            _hearts.Render(newValue);
        }

        private void OnTimeIsUp()
        {
            _text.SetInfo(TimeIsUpText);
        }
    }
}