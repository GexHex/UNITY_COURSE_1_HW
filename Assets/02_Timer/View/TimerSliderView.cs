using UnityEngine;
using UnityEngine.UI;

namespace Timer
{
    public class TimerSliderView : MonoBehaviour
    {
        [SerializeField] private Slider _timerSlider;

        public void SetValue(float time)
        {
            _timerSlider.value = time;
        }
    }
}