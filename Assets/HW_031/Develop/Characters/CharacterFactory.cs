using Cinemachine;
using UnityEngine;

public class CharacterFactory : MonoBehaviour
{
    private ControllersUpdateService _controllersUpdateService;
    private ControllersFactory _controllerFactory;
    private CharactersFactory _charactersFactory;
    
    private WeaponFactory _weaponFactory;
    private WeaponUpdateService _weaponUpdateService;

    public CharacterFactory(
    ControllersUpdateService controllersUpdateService,
    ControllersFactory controllerFactory,
    CharactersFactory charactersFactory,
    WeaponFactory weaponFactory,
    WeaponUpdateService weaponUpdateService)
    {
        _controllersUpdateService = controllersUpdateService;
        _controllerFactory = controllerFactory;
        _charactersFactory = charactersFactory;
        _weaponFactory = weaponFactory;
        _weaponUpdateService = weaponUpdateService;
    }

    public Character Create(MainHeroConfig config, Vector3 spawnPosition)
    {
        Character instance = _charactersFactory.CreateCharacter(config.Prefab, spawnPosition, config.MoveSpeed, config.RotationSpeed, config.Health);

        CinemachineVirtualCamera followCameraPrefab = Resources.Load<CinemachineVirtualCamera>("FollowCamera");
        CinemachineVirtualCamera followCamera = Object.Instantiate(followCameraPrefab);
        followCamera.Follow = instance.CameraTarget;

        Controller controller = _controllerFactory.CreateMainHeroPlayerController(instance);

        controller.Enable();

        _controllersUpdateService.Add(controller, () => instance.IsDestroyed);

        Gun gun = _weaponFactory.CreateGun(config.GunPrefab, instance.GunPosition, instance);
        _weaponUpdateService.SetGun(gun);

        return instance;
    }
}