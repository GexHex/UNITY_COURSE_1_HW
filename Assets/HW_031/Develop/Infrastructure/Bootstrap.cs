using System.Collections;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private LoadingScreen _loadingScreen;
    [SerializeField] private ConfirmPopup _confirmPopup;
    [SerializeField] private GameResult _gameRusult;

    private ControllersUpdateService _controllersUpdateService;
    private GameplayCycle _gameplayCycle;

    private WeaponUpdateService _weaponUpdateService;


    private void Awake()
    {
        StartCoroutine(StartProcess());
    }

    private IEnumerator StartProcess()
    {
        _loadingScreen.Show();
        _loadingScreen.ShowMessage("Loading...");

        //--------------------------- Создание вспомогательных сервисов 

        MainHeroConfig heroConfig = Resources.Load<MainHeroConfig>("Configs/MainHeroConfig");
        LevelConfig levelConfig = Resources.Load<LevelConfig>("Configs/LevelConfig");

        _controllersUpdateService = new();
        _weaponUpdateService = new();

        ControllersFactory controllersFactory = new();
        CharactersFactory charactersFactory = new();
        WeaponFactory weaponFactory = new();

        CharacterFactory mainHeroFactory = new(_controllersUpdateService, controllersFactory, charactersFactory, weaponFactory, _weaponUpdateService);
        EnemiesFactory enemiesFactory = new(_controllersUpdateService, controllersFactory, charactersFactory);

        EnemiesSpawner enemiesSpawner = new EnemiesSpawner(enemiesFactory);

        _gameplayCycle = new GameplayCycle(mainHeroFactory, heroConfig, levelConfig, _confirmPopup, _gameRusult, enemiesSpawner, this);

        yield return new WaitForSeconds(2.0f);  //Симуляция инициализации

        //--------------------------- Подготовка игры

        yield return _gameplayCycle.Prepare();

        _loadingScreen.Hide();

        //--------------------------- Старт Игры

        yield return _gameplayCycle.Launch();       
    }

    private void OnDestroy()
    {
        _gameplayCycle?.Dispose();
    }

    private void Update()
    {
        _controllersUpdateService?.Update(Time.deltaTime);
        _weaponUpdateService?.Update();
        _gameplayCycle?.Update(Time.deltaTime);
    }
}