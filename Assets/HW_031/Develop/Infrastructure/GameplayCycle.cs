using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayCycle : IDisposable
{
    private Character _mainHero;
    private GameMode _gameMode;

    private CharacterFactory _mainHeroFactory;
    private MainHeroConfig _mainHeroConfig;
    private LevelConfig _levelConfig;
    private ConfirmPopup _confirmPopup;
    private GameResult _gameResult;
    private EnemiesSpawner _enemiesSpawner;
    private MonoBehaviour _context;

    public GameplayCycle(
        CharacterFactory mainHeroFactory,
        MainHeroConfig mainHeroConfig,
        LevelConfig levelConfig,
        ConfirmPopup confirmPopup,
        GameResult gameResult,
        EnemiesSpawner enemiesSpawner,
        MonoBehaviour context)
    {
        _mainHeroFactory = mainHeroFactory;
        _mainHeroConfig = mainHeroConfig;
        _levelConfig = levelConfig;
        _confirmPopup = confirmPopup;
        _gameResult = gameResult;
        _enemiesSpawner = enemiesSpawner;
        _context = context;
    }

    public IEnumerator Prepare()
    {
        yield return SceneManager.LoadSceneAsync(_levelConfig.EnviromentSceneName, LoadSceneMode.Additive);

        _mainHero = _mainHeroFactory.Create(_mainHeroConfig, _levelConfig.MainHeroStartPosition);
    }

    private void ResetHero()
    {
        _mainHero.DestroySelf();
        _mainHero = _mainHeroFactory.Create(_mainHeroConfig, _levelConfig.MainHeroStartPosition);
    }

    public IEnumerator Launch()
    {
        _confirmPopup.Show();
        _confirmPopup.ShowMessage($"Press {KeyCode.F.ToString()} for begin");

        yield return _confirmPopup.WaitConfirm(KeyCode.F);

        _confirmPopup.Hide();

        _gameMode = new GameMode(_levelConfig, _mainHero, _enemiesSpawner);

        _gameMode.Win += OnGameModeWin;
        _gameMode.Defeat += OnGameModeDefeat;

        _gameMode.Start();
    }

    public IEnumerator Result(Color color, string message)
    {
        _gameResult.Show();
        _gameResult.ShowResult(message);
        _gameResult.ShowMessage($"Press {KeyCode.F.ToString()} for begin");
        _gameResult.ChangeBackgroundColor(color);

        yield return _confirmPopup.WaitConfirm(KeyCode.F);

        _gameResult.Hide();

        ResetHero();

        _context.StartCoroutine(Launch());
    }

    public void Update(float deltaTime)
    {
        _gameMode?.Update(deltaTime);
    }

    private void OnGameModeEnded()
    {
        if (_gameMode != null)
        {
            _gameMode.Win -= OnGameModeWin;
            _gameMode.Defeat -= OnGameModeDefeat;
        }
    }

    public void Dispose()
    {
        OnGameModeEnded();
    }

    private void OnGameModeDefeat()
    {
        OnGameModeEnded();

        _context.StartCoroutine(Result(new Color(0.31f, 0.12f, 0.3f), "DEFEAT!"));

        Debug.Log("Defeat");
    }

    private void OnGameModeWin()
    {
        OnGameModeEnded();

        _context.StartCoroutine(Result(new Color(0.2f, 0.45f, 0.25f), "WIN!"));

        Debug.Log("Win");
    }
}