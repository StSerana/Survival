namespace Survival
{
    public class Game
    {
        public static ICell[,] Map;
        public static bool IsOver;

        public static int MapWidth => Map.GetLength(0);
        public static int MapHeight => Map.GetLength(1);

        public static void CreateMap(int width, int height, int playersCount)
        {
            Map = MapCreator.CreateMap(width, height, playersCount);
        }

        public void CheckWinner()
        {
            
        }
    }
}