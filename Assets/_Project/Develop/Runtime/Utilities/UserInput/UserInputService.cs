using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Utilities.UserInput
{
    public class UserInputService
    {
        public event Action<List<char>> InputCompleted;

        private readonly List<char> _userInput = new();

        private bool _isReading;
        private bool _onlyDigits;

        public IReadOnlyList<char> UserInput => _userInput;

        public void StartDigitsInput()
        {
            _userInput.Clear();
            _onlyDigits = true;
            _isReading = true;

            Debug.Log("Начат ввод цифр");
        }

        public void StartLettersInput()
        {
            _userInput.Clear();
            _onlyDigits = false;
            _isReading = true;

            Debug.Log("Начат ввод букв");
        }

        public void Update()
        {
            if (_isReading == false)
                return;

            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                FinishInput();
                return;
            }

            foreach (char symbol in UnityEngine.Input.inputString)
            {
                if (_onlyDigits)
                {
                    if (char.IsDigit(symbol))
                    {
                        _userInput.Add(symbol);
                        Debug.Log($"Добавлен символ: {symbol}");
                    }
                }
                else
                {
                    if (char.IsLetter(symbol))
                    {
                        _userInput.Add(symbol);
                        Debug.Log($"Добавлен символ: {symbol}");
                    }
                }
            }
        }

        private void FinishInput()
        {
            _isReading = false;

            Debug.Log($"Ввод завершен: {new string(_userInput.ToArray())}");

            InputCompleted?.Invoke(new List<char>(_userInput));
        }
    }
}