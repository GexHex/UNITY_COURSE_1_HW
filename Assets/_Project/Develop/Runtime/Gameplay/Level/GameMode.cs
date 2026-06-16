using System;
using System.Collections.Generic;

namespace Assets._Project.Develop.Runtime.Gameplay.Level
{
    public class GameMode
    {
        public event Action Win;
        public event Action Defeat;

        public void CheckResult(IReadOnlyList<char> rightSymbols, IReadOnlyList<char> userInputSymbols)
        {
            if (rightSymbols.Count != userInputSymbols.Count)
            {
                ProcessDefeat();
                return;
            }

            for (int i = 0; i < rightSymbols.Count; i++)
            {
                if (rightSymbols[i] != userInputSymbols[i])
                {
                    ProcessDefeat();
                    return;
                }
            }

            ProcessWin();
        }

        private void ProcessWin()
        {
            ProcessEndGame();

            Win?.Invoke();
        }

        private void ProcessDefeat()
        {
            ProcessEndGame();

            Defeat?.Invoke();
        }

        private void ProcessEndGame()
        {
            
        }
    }
}