using Assets._Project.Develop.Runtime.UI.CommonViews;
using Assets._Project.Develop.Runtime.UI.Core;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Project.Develop.Runtime.UI.MainMenu
{
    public class MainMenuScreenView : MonoBehaviour, IView
    {
        public event Action ResetStatsButtonClicked;
        public event Action DigitsButtonClicked;
        public event Action LeterstButtonClicked;

        [field: SerializeField] public IconTextListView StatsView { get; private set; }
        [field: SerializeField] public IconTextListView WalletView { get; private set; }


        [SerializeField] private Button _resetStatsButton;
        [SerializeField] private Button _digitsButton;
        [SerializeField] private Button _leterstButton;

        private void OnEnable()
        {
            _resetStatsButton.onClick.AddListener(OnResetStatsButtonClicked);
            _digitsButton.onClick.AddListener(OnDigitsButtonClicked);
            _leterstButton.onClick.AddListener(OnLeterstButtonClicked);
        }

        private void OnDisable()
        {
            _resetStatsButton.onClick.RemoveListener(OnResetStatsButtonClicked);
            _digitsButton.onClick.RemoveListener(OnDigitsButtonClicked);
            _leterstButton.onClick.RemoveListener(OnLeterstButtonClicked);
        }

        private void OnDigitsButtonClicked() => DigitsButtonClicked?.Invoke();
        private void OnLeterstButtonClicked() => LeterstButtonClicked?.Invoke();
        private void OnResetStatsButtonClicked() => ResetStatsButtonClicked?.Invoke();
    }
}
