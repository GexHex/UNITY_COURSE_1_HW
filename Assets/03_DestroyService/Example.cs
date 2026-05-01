using TMPro;
using UnityEngine;

namespace DestroyService
{
    public class Example : MonoBehaviour
    {
        [SerializeField] private InputUser _inputUser;
        [SerializeField] private TMP_Text _text;
        [SerializeField] private GameObject _prefabEnemy1;
        [SerializeField] private GameObject _prefabEnemy2;
        [SerializeField] private GameObject _prefabEnemy3;
        private DestroyService _destroyService;
        private readonly int _minPositionRange = -3;
        private readonly int _maxPositionRange = 3;

        private void Awake()
        {
            _destroyService = new DestroyService();

            _inputUser.EntityCreated01 += OnCreateEnemy1;
            _inputUser.EntityCreated02 += OnCreateEnemy2;
            _inputUser.EntityCreated03 += OnCreateEnemy3;
        }

        private void OnDestroy()
        {
            _inputUser.EntityCreated01 -= OnCreateEnemy1;
            _inputUser.EntityCreated02 -= OnCreateEnemy2;
            _inputUser.EntityCreated03 -= OnCreateEnemy3;
        }

        private void Update()
        {
            _destroyService.Process();

            _text.text = _destroyService.EnemyNumber.ToString();
        }

        private void OnCreateEnemy1() => _destroyService.AddEnemy(CreateObject(_prefabEnemy1), DeadRuleBoolIsDead);
        private void OnCreateEnemy2() => _destroyService.AddEnemy(CreateObject(_prefabEnemy2), DeadRuleBirthTime);
        private void OnCreateEnemy3() => _destroyService.AddEnemy(CreateObject(_prefabEnemy3), DeadRuleToMuch);

        private Enemy CreateObject(GameObject prefab)
        {
            GameObject newObject = Instantiate(prefab, GetRamdomPosition(), Quaternion.identity);
            newObject.name = prefab.name;

            Enemy enemy = newObject.AddComponent<Enemy>();
            enemy.BirthTime = Time.time;
            enemy.DeathTime = Time.time;

            if (prefab == _prefabEnemy1)
                enemy.Type = 1;
            else if (prefab == _prefabEnemy2)
                enemy.Type = 2;
            else
                enemy.Type = 3;

            Debug.Log($"Время рождения: {enemy.BirthTime}");

            return enemy;
        }

        bool DeadRuleBoolIsDead(Enemy enemy)
        {
            return enemy.IsDead = true && (Time.time - enemy.DeathTime > 1f);
        }

        bool DeadRuleBirthTime(Enemy enemy)
        {
            float timePassed = Time.time - enemy.BirthTime;
            return timePassed > 3f;
        }

        bool DeadRuleToMuch(Enemy enemy)
        {
            int count = 0;

            foreach (Enemy enemys in _destroyService.Enemies)
            {
                if (enemys.gameObject.name == _prefabEnemy3.name)
                    count++;
            }

            return count > 5;
        }

        private Vector3 GetRamdomPosition()
        {
            Vector3 position = new(Random.Range(_minPositionRange, _maxPositionRange), 0, Random.Range(_minPositionRange, _maxPositionRange));
            return position;
        }
    }
}