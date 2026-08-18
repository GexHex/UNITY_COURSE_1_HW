using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore.Systems;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using System;
using TMPro;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.InfoDisplay
{
    public class InfoTextSystem : IInitializableSystem, IDisposableSystem
    {
        private TMP_Text _healthText;
        private ReactiveVariable<float> _currentHealth;
        private ReactiveVariable<float> _maxHealth;

        private IDisposable _healthDisposable;

        public void OnInit(Entity entity)
        {
            _healthText = entity.GetComponent<InfoText>().Value;
            _currentHealth = entity.CurrentHealth;
            _maxHealth = entity.MaxHealth;

            UpdateText();

            _healthDisposable = _currentHealth.Subscribe(OnHealthChanged);
        }

        public void OnDispose()
        {
            _healthDisposable.Dispose();
        }

        private void OnHealthChanged(float previousHealth, float currentHealth)
        {
            UpdateText();
        }

        private void UpdateText()
        {
            _healthText.text = _currentHealth.Value.ToString();
        }
    }
}
