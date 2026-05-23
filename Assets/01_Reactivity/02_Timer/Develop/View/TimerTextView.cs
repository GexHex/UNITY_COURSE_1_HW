using TMPro;
using UnityEngine;

namespace Timer
{
    public class TimerTextView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _timerTextInfo;
        [SerializeField] private TMP_Text _timerValue;

        public string CurrentInfo => _timerTextInfo.text;

        public void SetInfo(string text)
        {
            _timerTextInfo.text = text;
        }

        public void SetTime(float time)
        {
            if (time <= 0)
                _timerValue.text = "0";

            _timerValue.text = time.ToString("F2");
        }
    }
}