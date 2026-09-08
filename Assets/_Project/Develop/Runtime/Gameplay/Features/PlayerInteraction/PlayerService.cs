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
        private readonly EntitiesLifeContext _entitiesLifeContext;
        private readonly ReactiveVariable<PlayerModes> _mode = new(PlayerModes.None);

        private Entity _player;

        public PlayerService(
            IInputService inputService,
            EntitiesFactory entitiesFactory,
            WalletService walletService,
            LevelConfig levelConfig,
            EntitiesLifeContext entitiesLifeContext)
        {
            _inputService = inputService;
            _entitiesFactory = entitiesFactory;
            _walletService = walletService;
            _levelConfig = levelConfig;
            _entitiesLifeContext = entitiesLifeContext;
        }

        public IReadOnlyVariable<PlayerModes> Mode => _mode;

        public void Initialize()
        {
            _player = _entitiesFactory.CreatePlayer(Vector3.zero);
        }

        public void SetMode(PlayerModes mode) => _mode.Value = mode;

        public void Update()
        {
            TryInvokePointerMove();

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

        private void TryInvokePointerMove()
        {
            if (_player == null || _player.IsInit == false)
                return;

            if (_inputService.TryGetPointerPosition(out Vector2 screenPosition, out Vector3 gamePosition) == false)
                return;

            _player.PlayerPointerMoveEvent.Invoke(new PointerMoveArgs(screenPosition, gamePosition));
        }

        private void TryExplode(Vector3 worldPosition)
        {
            if (_player == null || _player.IsInit == false)
                return;

            float radius = _levelConfig.ExplosionConfig.Radius;
            float sqrRadius = radius * radius;
            float damage = _levelConfig.ExplosionConfig.Damage;

            for (int i = 0; i < _entitiesLifeContext.Entities.Count; i++)
            {
                Entity target = _entitiesLifeContext.Entities[i];

                if (target == _player)
                    continue;

                if (target.IsInit == false)
                    continue;

                if (target.TryGetTransform(out Transform transform) == false)
                    continue;

                if ((transform.position - worldPosition).sqrMagnitude > sqrRadius)
                    continue;

                EntitiesHelper.TryTakeDamageFrom(_player, target, damage);
            }

            _player.PlayerExplosionEvent.Invoke(worldPosition);
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
                Teams.Heroes);

            Debug.Log($"Мина поставлена в {worldPosition}, золота осталось {_walletService.GetCurrency(CurrencyTypes.Gold).Value}");
        }
    }
}
