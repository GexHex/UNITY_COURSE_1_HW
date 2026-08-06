using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Infrastructure.DI;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay
{
    public class TestGameplay : MonoBehaviour
    {
        private DIContainer _container;
        private EntitiesFactory _entitiesFactory;
        private Entity _entity;

        private bool _isRunning;

        public void Initialize(DIContainer container)
        {
            _container = container;
            _entitiesFactory = _container.Resolve<EntitiesFactory>();
        }

        public void Run()
        {
            for (int i = -5; i < 6; i++)
                for (int j = 0; j < 5; j++)
                    _entitiesFactory.CreateGhost(new Vector3(i * 2, 0, j * 2));

            _entity = _entitiesFactory.CreateHero(new Vector3(0, 0, -5));

            _isRunning = true;
        }

        private void Update()
        {
            if (_isRunning == false)
                return;              

            if (Input.GetKeyDown(KeyCode.Space))
            {
                _entity.RunTeleportEvent.Invoke();
                _entity.RunAreaDamageEvent.Invoke();
            }
        }
    }
}
