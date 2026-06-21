using Assets._Project.Develop.Runtime.Gameplay.Infrastructure;
using System;

namespace Assets._Project.Develop.Runtime.Utilities.UI
{
    public class GameModeChooseService
    {
        public event Action<GameplayInputArgs> GameModeSelected;

        public void SelectDigits()
        {
            GameModeSelected?.Invoke(new GameplayInputArgs(true));
        }

        public void SelectLetters()
        {
            GameModeSelected?.Invoke(new GameplayInputArgs(false));
        }
    }
}