using Assets._Project.Develop.Runtime.UI.Wrappers;
using System;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.UI.Menu
{
    public class UIMainMenu : MonoBehaviour
    {
        [SerializeField] private UIButtonWrapper _digits;
        [SerializeField] private UIButtonWrapper _letters;

        public event Action DigitsClicked;
        public event Action LettersClicked;

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
            DigitsClicked?.Invoke();
            Debug.Log("Нажата кнопка цифр");
        }

        private void PressLetters()
        {
            LettersClicked?.Invoke();
            Debug.Log("Нажата кнопка букв");
        }
    }
}