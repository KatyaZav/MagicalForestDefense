namespace DI.Game.Develop.CommonServices.SceneManagment
{
    public interface IInputSceneArgs
    {
    }

    public class GameplayInputArgs : IInputSceneArgs
    {
        public GameplayInputArgs()
        {
            //LevelNumber = levelNumber;
        }

        //public int LevelNumber { get; }
    }

    public class MainMenuInputArgs : IInputSceneArgs
    {
    }
}
