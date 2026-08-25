using Assets._Project.Develop.Runtime.UI.CommonViews;
using Assets._Project.Develop.Runtime.UI.Core;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.UI.Gameplay
{
    public class GameplayScreenView : MonoBehaviour, IView
    {
        [field: SerializeField] public IconTextListView WalletView { get; private set; }
        [field: SerializeField] public TextView BuildingHealthView { get; private set; }
        [field: SerializeField] public TextView WaveView { get; private set; }
        [field: SerializeField] public TextView RestTimerView { get; private set; }
        [field: SerializeField] public TextView ModeView { get; private set; }
    }
}
