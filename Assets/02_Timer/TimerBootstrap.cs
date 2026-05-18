using UnityEngine;

namespace Timer
{
    public class TimerBootstrap : MonoBehaviour
    {
        [SerializeField] private TimerTestingInput _inputUser;        
        [SerializeField] private TimerController _timerController;
        [SerializeField] private TimerView _timerView;

        private Timer _timer;        

        private void Awake()
        {
            _timer = new Timer();
            _timerView.Initialize(_timer);
            _timerController.Initialize(_inputUser, _timer);
        }
    }
}