using Assets._Project.Develop.Runtime.UI.Core;
using TMPro;
using UnityEngine;

public class GameplayScreenView : MonoBehaviour, IView
{
    [field: SerializeField] public TMP_Text GeneratedText { get; private set; }
    [field: SerializeField] public TMP_Text UserAnswer { get; private set; }
}