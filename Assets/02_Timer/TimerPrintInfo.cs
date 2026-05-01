using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Timer
{
    public class TimerPrintInfo : MonoBehaviour
    {
        private const string StartText = "Start";
        private const string StopText = "Stop";
        private const string ResetText = "Stop (Reset)";
        private const string TimeIsUpText = "Time is Up!";

        [SerializeField] private TMP_Text _timerNumbers;
        [SerializeField] private TMP_Text _timerInfo;
        [SerializeField] private Slider _slider;
        [SerializeField] private Transform _canvasTransform;
        [SerializeField] private GameObject _heart;

        private Timer _timer;
        private List<GameObject> _hearts;
        private int _maxHearts = 10;
        private float _currentTime;
        private int _roundedTime;
        private int _lastHeartsCount;
        private float _offsetPosition = 100;
        private Vector2 _heartPosition = new Vector2(-460, 150); 

        public void Initialize(Timer timer)
        {
            _timer = timer;

            _timer.Started += onTimerStarted;
            _timer.Stopped += onTimerStopped;
            _timer.Changed += onTimeChanged;
            _timer.Reset += onTimerReset;
            _timer.TimeIsUp += onTimerIsUp;

            ForceInitUI(_timer.CurrentTime);
        }

        private void OnDestroy()
        {
            _timer.Started -= onTimerStarted;
            _timer.Stopped -= onTimerStopped;
            _timer.Changed -= onTimeChanged;
            _timer.Reset -= onTimerReset;
            _timer.TimeIsUp -= onTimerIsUp;
        }

        private void ForceInitUI(float time)
        {
            _currentTime = time;

            _roundedTime = Mathf.FloorToInt(_currentTime);
            _lastHeartsCount = _roundedTime;

            SetValuesOnUI();
            DestroyHearts();
            SetHeartsOnUI();
        }

        private void onTimerIsUp()
        {
            _timerInfo.text = TimeIsUpText;
        }

        private void onTimerStarted()
        {
            _timerInfo.text = StartText;
        }

        private void onTimerStopped()
        {
            if (_timerInfo.text == ResetText)
                return;

            if (_timerInfo.text == TimeIsUpText)
                return;

            _timerInfo.text = StopText;
        }

        private void onTimerReset(float time)
        {
            _timerInfo.text = ResetText;

            _currentTime = time;

            SetValuesOnUI();

            _roundedTime = Mathf.FloorToInt(_currentTime);
            _lastHeartsCount = _roundedTime;

            DestroyHearts();
            SetHeartsOnUI();
        }

        private void onTimeChanged(float time)
        {
            _currentTime = time;

            SetValuesOnUI();
            CreateHearts();
        }

        private void SetValuesOnUI()
        {
            _timerNumbers.text = _currentTime.ToString("F2");
            _slider.value = _currentTime;
        }

        private void CreateHearts()
        {
            _roundedTime = Mathf.FloorToInt(_currentTime);

            if (_roundedTime != _lastHeartsCount)
            {
                SetHeartsOnUI();
                _lastHeartsCount = _roundedTime;
            }
        }

        private void SetHeartsOnUI()
        {
            int targetCount = _roundedTime;

            targetCount = Mathf.Clamp(targetCount, 0, _maxHearts);

            if (_hearts == null)
                _hearts = new List<GameObject>();

            while (_hearts.Count > targetCount)
            {
                GameObject heart = _hearts[_hearts.Count - 1];
                _hearts.RemoveAt(_hearts.Count - 1);
                Destroy(heart);
            }

            while (_hearts.Count < targetCount)
            {
                GameObject heart = Instantiate(_heart, _canvasTransform);
                _hearts.Add(heart);
            }

            for (int i = 0; i < _hearts.Count; i++)
            {
                RectTransform rect = _hearts[i].GetComponent<RectTransform>();
                Vector2 currentPosition = new Vector2(i * _offsetPosition, 0);
                Vector2 offsetPosition = currentPosition + _heartPosition;
                rect.anchoredPosition = offsetPosition;
            }
        }

        private void DestroyHearts()
        {
            if (_hearts == null)
                return;

            for (int i = 0; i < _hearts.Count; i++)
            {
                Destroy(_hearts[i]);
            }

            _hearts.Clear();
        }
    }
}