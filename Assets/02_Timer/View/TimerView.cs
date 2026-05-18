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
            _timer.Changed += OnChanged;
            _timer.TimeIsUp += OnTimeIsUp;

            ForceInit(_timer.CurrentTime);
        }

        private void OnDestroy()
        {
            if (_timer == null)
                return;

            _timer.Started -= OnStarted;
            _timer.Stopped -= OnStopped;
            _timer.Reset -= OnReset;
            _timer.Changed -= OnChanged;
            _timer.TimeIsUp -= OnTimeIsUp;
        }

        private void ForceInit(float time)
        {
            _text.SetTime(time);
            _slider.SetValue(_timer.CurrentTime);
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

        private void OnReset(float time)
        {
            _text.SetInfo(ResetText);
            _text.SetTime(time);
            _slider.SetValue(_timer.CurrentTime);
            _hearts.Clear();
            _hearts.Render(time);
        }

        private void OnChanged(float time)
        {
            _text.SetTime(time);
            _slider.SetValue(_timer.CurrentTime);
            _hearts.Render(time);
        }

        private void OnTimeIsUp()
        {
            _text.SetInfo(TimeIsUpText);
        }
    }
}