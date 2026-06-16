using Assets._Project.Develop.Runtime.UI.Wrappers;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.UI.Menu
{
    public class UIMainMenu : MonoBehaviour
    {
        [SerializeField] private UIButtonWrapper _digits;
        [SerializeField] private UIButtonWrapper _letters;

        private void Awake()
        {
            _digits.Clicked += PressDigits;
            _letters.Clicked += PressLetters;
        }

        private void OnDestroy()
        {
            _digits.Clicked -= PressDigits;
            _letters.Clicked -= PressLetters;
        }

        private void PressDigits()
        {
            Debug.Log("Нажата кнопка цифр");
        }

        private void PressLetters()
        {
            Debug.Log("Нажата кнопка букв");
        }
    }
}