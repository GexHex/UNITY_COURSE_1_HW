using TMPro;
using UnityEngine;

namespace DestroyService
{
    public class Example : MonoBehaviour
    {
        [SerializeField] private UserInput _inputUser;
        [SerializeField] private TMP_Text _text;
        [SerializeField] private GameObject _dragonPrefab;
        [SerializeField] private GameObject _elfPrefab;
        [SerializeField] private GameObject _ogrPrefab;
        private DestroyService _destroyService;
        private readonly int _minPositionRange = -3;
        private readonly int _maxPositionRange = 3;

        private void Awake()
        {
            _destroyService = new DestroyService();

            _inputUser.DragonCreated += OnCreateDragon;
            _inputUser.ElfCreated += OnCreateElf;
            _inputUser.OgrCreated += OnCreateOrg;
        }

        private void OnDestroy()
        {
            _inputUser.DragonCreated -= OnCreateDragon;
            _inputUser.ElfCreated -= OnCreateElf;
            _inputUser.OgrCreated -= OnCreateOrg;
        }

        private void Update()
        {
            _destroyService.Process();

            _text.text = _destroyService.EnemyNumber.ToString();
        }

        private void OnCreateDragon()
        {
            Enemy newEnemy = CreateObject(_dragonPrefab);

            _destroyService.AddEnemy(newEnemy, () => DeadRuleBoolIsDead(newEnemy));
        }

        private void OnCreateElf()
        {
            Enemy newEnemy = CreateObject(_elfPrefab);

            _destroyService.AddEnemy(newEnemy, () => DeadRuleBirthTime(newEnemy));
        }

        private void OnCreateOrg()
        {
            Enemy newEnemy = CreateObject(_ogrPrefab);

            _destroyService.AddEnemy(newEnemy, () => DeadRuleToMuch(newEnemy));
        }

        private Enemy CreateObject(GameObject prefab)
        {
            GameObject newObject = Instantiate(prefab, GetRamdomPosition(), Quaternion.identity);
            newObject.name = prefab.name;

            Enemy enemy = newObject.AddComponent<Enemy>();
            enemy.BirthTime = Time.time;
            enemy.DeathTime = Time.time;

            if (prefab == _dragonPrefab)
                enemy.Type = 1;
            else if (prefab == _elfPrefab)
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

            foreach (var pair in _destroyService.Rules)
            {
                Enemy currentEnemy = pair.Key;

                if (currentEnemy.gameObject.name == _ogrPrefab.name)
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