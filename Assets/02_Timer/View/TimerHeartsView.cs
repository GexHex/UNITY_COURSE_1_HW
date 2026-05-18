using System.Collections.Generic;
using UnityEngine;

namespace Timer
{
    public class TimerHeartsView : MonoBehaviour
    {
        [SerializeField] private Transform _container;
        [SerializeField] private GameObject _heartPrefab;
        [SerializeField] private int _maxHearts = 10;
        [SerializeField] private float _offset = 100;

        private readonly List<GameObject> _hearts = new();

        public void Render(float time)
        {
            int targetCount = Mathf.Clamp(Mathf.FloorToInt(time), 0, _maxHearts);

            UpdateHeartsCount(targetCount);
            UpdatePositions();
        }

        public void Clear()
        {
            foreach (GameObject heart in _hearts)
                Destroy(heart);

            _hearts.Clear();
        }

        private void UpdateHeartsCount(int targetCount)
        {
            while (_hearts.Count > targetCount)
            {
                GameObject heart = _hearts[^1];

                _hearts.RemoveAt(_hearts.Count - 1);

                Destroy(heart);
            }

            while (_hearts.Count < targetCount)
            {
                GameObject heart = Instantiate(_heartPrefab, _container);

                _hearts.Add(heart);
            }
        }

        private void UpdatePositions()
        {
            for (int i = 0; i < _hearts.Count; i++)
            {
                RectTransform rect = _hearts[i].GetComponent<RectTransform>();

                rect.anchoredPosition = new Vector2(i * _offset, 0);
            }
        }
    }
}