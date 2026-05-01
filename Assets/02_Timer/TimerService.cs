using UnityEngine;

namespace Timer
{
    public class TimerService : MonoBehaviour
    {
        [SerializeField] private InputUser _inputUser;
        [SerializeField] private TimerPrintInfo _timerPrintInfo;
        [SerializeField] private TimerController _timerController;
        private Timer _timer;        

        private void Awake()
        {
            _timer = new Timer();
            _timerPrintInfo.Initialize(_timer);
            _timerController.Initialize(_inputUser, _timer);
        }
    }
}