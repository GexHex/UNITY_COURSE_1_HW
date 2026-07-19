using UnityEngine;

namespace Assets._Project.Develop.Runtime.Meta.Features
{
    public class StatsInfo
    {
        public void ShowStats(int wins, int losses, int gold)
        {
            Debug.Log($"-----------------Stats-----------------");
            Debug.Log($"Золото: {gold}");
            Debug.Log($"Побед: {wins}");
            Debug.Log($"Поражений: {losses}");
            Debug.Log($"---------------------------------------");
        }

        public void ShowError()
        {
            Debug.LogError("Денег для сброса кошелька нет!");
        }
    }
}