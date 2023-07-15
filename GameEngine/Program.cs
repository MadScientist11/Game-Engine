using Game;

namespace GameEngine
{
    public class Program
    {

        public static void Main(string[] args)
        {
            GameWindow gameWindow = new GameWindow();
            gameWindow.Initialize();
            gameWindow.Run();
        }

      
    }
};