using Assets._Project.Develop.Runtime.Utilities.SceneManagment;

namespace Assets._Project.Develop.Runtime.Gameplay.Infrastructure
{
    public class GameplayInputArgs : IInputSceneArgs
    {
        public bool IsDigits { get; }

        public GameplayInputArgs(bool isDigits)
        {
            IsDigits = isDigits;
        }

        public int LevelNumber { get; }
    }
}