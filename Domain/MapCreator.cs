namespace Survival
{
    public class MapCreator
    {
        public static ICell[,] CreateMap(int width, int height, int playersCount)
        {
            var map = new ICell[width, height];
            for (var x = 0; x < width; x++)
            for (var y = 0; y < height; y++)
            {
                map[x, y] = new ColorCell(State.Empty, Color.Gray);
            }
            map[0, 0] = new ColorCell(State.Three, Color.Red);
            map[width - 1, height - 1] = new ColorCell(State.Three, Color.Blue);
                
            return map;
        }
    }
}