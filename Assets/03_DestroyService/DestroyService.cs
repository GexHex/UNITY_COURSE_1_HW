using System;
using System.Collections.Generic;
using UnityEngine;

namespace DestroyService
{
    public class DestroyService
    {
        private Dictionary<Enemy, Func<bool>> _enemyDestroyRules = new();
        
        public Dictionary<Enemy, Func<bool>> Rules => _enemyDestroyRules;

        public int EnemyNumber => _enemyDestroyRules.Count;

        public void AddEnemy(Enemy newEnemy, Func<bool> newRule)
        {
            _enemyDestroyRules.Add(newEnemy, newRule);

            Debug.Log($"Новый враг добавлен в сервис: {newEnemy.name}");
        }

        public void Process()
        {
            List<Enemy> enemiesToRemove = new();

            foreach (var pair in _enemyDestroyRules)
            {
                Enemy enemy = pair.Key;

                Func<bool> rule = pair.Value;

                if (rule())
                    enemiesToRemove.Add(enemy);
            }

            foreach (Enemy enemy in enemiesToRemove)
            {
                GameObject.Destroy(enemy.gameObject);

                _enemyDestroyRules.Remove(enemy);
            }
        }
    }
}