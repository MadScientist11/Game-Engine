

namespace Game
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