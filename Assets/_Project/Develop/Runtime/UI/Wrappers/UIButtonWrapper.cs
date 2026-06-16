using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Project.Develop.Runtime.UI.Wrappers
{
    public class UIButtonWrapper : MonoBehaviour
    {
        [SerializeField] private Button _button;

        public event Action Clicked;

        private void Awake()
        {
            _button.onClick.AddListener(OnButtonClicked);
        }

        private void OnButtonClicked()
        {
            Clicked?.Invoke();
        }
    }
}