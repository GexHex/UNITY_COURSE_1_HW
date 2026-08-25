using Assets._Project.Develop.Runtime.Configs.Gameplay.Levels;
using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Gameplay.Features.BuildingFeature;
using Assets._Project.Develop.Runtime.Gameplay.Features.InputFeature;
using Assets._Project.Develop.Runtime.Gameplay.Features.TeamsFeature;
using Assets._Project.Develop.Runtime.Meta.Features.Wallet;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.PlayerInteraction
{
    public class PlayerService
    {
        private readonly IInputService _inputService;
        private readonly EntitiesFactory _entitiesFactory;
        private readonly WalletService _walletService;
        private readonly LevelConfig _levelConfig;
        private readonly BuildingHolderService _buildingHolderService;
        private readonly ReactiveVariable<PlayerModes> _mode = new(PlayerModes.None);

        public PlayerService(
            IInputService inputService,
            EntitiesFactory entitiesFactory,
            WalletService walletService,
            LevelConfig levelConfig,
            BuildingHolderService buildingHolderService)
        {
            _inputService = inputService;
            _entitiesFactory = entitiesFactory;
            _walletService = walletService;
            _levelConfig = levelConfig;
            _buildingHolderService = buildingHolderService;
        }

        public IReadOnlyVariable<PlayerModes> Mode => _mode;

        public void SetMode(PlayerModes mode) => _mode.Value = mode;

        public void Update()
        {
            if (_inputService.IsEnabled == false)
                return;

            if (_inputService.IsClickDown == false)
                return;

            if (_inputService.TryGetClickWorldPosition(out Vector3 worldPosition) == false)
                return;

            switch (_mode.Value)
            {
                case PlayerModes.Explode:
                    TryExplode(worldPosition);
                    break;

                case PlayerModes.PlaceMine:
                    TryPlaceMine(worldPosition);
                    break;
            }
        }

        private void TryExplode(Vector3 worldPosition)
        {
            Entity owner = _buildingHolderService.Building;

            if (owner == null || owner.IsInit == false)
                return;

            _entitiesFactory.CreateExplosion(
                worldPosition,
                _levelConfig.ExplosionConfig.Radius,
                _levelConfig.ExplosionConfig.Damage,
                owner);
        }

        private void TryPlaceMine(Vector3 worldPosition)
        {
            if (_levelConfig.MineConfig == null)
            {
                Debug.LogError("У LevelConfig не назначен MineConfig!");

                return;
            }

            int cost = _levelConfig.MineConfig.Cost;

            if (_walletService.Enough(CurrencyTypes.Gold, cost) == false)
            {
                Debug.Log($"Недостаточно золота для мины. Нужно {cost}, есть {_walletService.GetCurrency(CurrencyTypes.Gold).Value}");
              
                return;
            }

            _walletService.Spend(CurrencyTypes.Gold, cost);

            worldPosition.y = 0.05f;

            _entitiesFactory.CreateMine(
                worldPosition,
                _levelConfig.MineConfig,
                Teams.Buildings);

            Debug.Log($"Мина поставлена в {worldPosition}, золота осталось {_walletService.GetCurrency(CurrencyTypes.Gold).Value}");
        }
    }
}
