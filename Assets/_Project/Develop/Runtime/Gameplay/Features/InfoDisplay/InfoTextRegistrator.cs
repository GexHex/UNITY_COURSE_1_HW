using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore.Mono;
using TMPro;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.InfoDisplay
{
    public class InfoTextRegistrator : MonoEntityRegistrator
    {
        [SerializeField] private TMP_Text _healthText;

        public override void Register(Entity entity)
        {
            entity.AddComponent(new InfoText() {Value = _healthText});
        }
    }
}
