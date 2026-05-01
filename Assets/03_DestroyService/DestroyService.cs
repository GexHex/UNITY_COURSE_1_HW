using System.Collections.Generic;
using UnityEngine;

namespace DestroyService
{
    public class DestroyService 
    {
        public List<Enemy> Enemies { get; private set; } = new List<Enemy>();
        public int EnemyNumber { get; private set; }
        private List<Rule> _rules = new List<Rule>(); 

        public void AddEnemy(Enemy newEnemy, Rule newRule)
        {
            Enemies.Add(newEnemy);
            _rules.Add(newRule);

            Debug.Log($"Новый враг добавлен в сервис");
        }

        public void Process()
        {            
            List<int> indicesToRemove = new List<int>(); 

            EnemyNumber = Enemies.Count;

            for (int i = 0; i < Enemies.Count; i++)
            {
                if (_rules[i](Enemies[i]))
                    indicesToRemove.Add(i);
            }
            
            for (int j = indicesToRemove.Count - 1; j >= 0; j--)
            {
                int index = indicesToRemove[j];
                GameObject.Destroy(Enemies[index].gameObject);

                Enemies.RemoveAt(index);
                _rules.RemoveAt(index);
            }
        }
    }
}