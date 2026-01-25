using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private Timer _timer;
    [SerializeField] private PrintInfo _printInfo;
    [SerializeField] private CoinsCounter _coinsCounter;
    [SerializeField] private CoinsCount _coinsCount;
    [SerializeField] private TransformPlayer _transformPlayer;
    [SerializeField] private DoubleJumpCheck _colliderDetector;

    [SerializeField] private float _maxTimeToLose;

    private int _maxCoinToWin;
    private float _totalTimeRound;
    private bool _isTimeWorking = true;

    private void Awake()
    {
        _maxCoinToWin = _coinsCount.Count;
    }

    private void Update()
    {
        if (_isTimeWorking)
            CheckToWin();
    }

    private void CheckToWin()
    {
        _timer.TimerStart();
        _totalTimeRound = _timer.TotalTimeRound;

        _printInfo.Time(_totalTimeRound);
        _printInfo.Score(_coinsCounter.Coins);

        if (_coinsCounter.Coins >= _maxCoinToWin)
        {
            _isTimeWorking = false;
            _transformPlayer.GameOver();
            _printInfo.Win(_coinsCounter.Coins);
        }

        else if (_totalTimeRound >= _maxTimeToLose && _coinsCounter.Coins == _maxCoinToWin)
        {
            _isTimeWorking = false;
            _transformPlayer.GameOver();
            _printInfo.Win(_coinsCounter.Coins);
        }

        else if (_totalTimeRound >= _maxTimeToLose && _coinsCounter.Coins < _maxCoinToWin)
        {
            _isTimeWorking = false;
            _transformPlayer.GameOver();
            _printInfo.GameOver(_coinsCounter.Coins);
        }
    }
}