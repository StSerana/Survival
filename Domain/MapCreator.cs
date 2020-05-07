namespace Survival
{
    public class MapCreator
    {
        public static ICell[,] CreateMap(int width, int height)
        {
            var map = new ICell[width, height];
            for (var x = 0; x < width; x++)
            for (var y = 0; y < height; y++)
            {
                if(x == 0 || y == 0 || x == width - 1 || y == height - 1)
                    map[x, y] = new Wall();
                map[x, y] = new ColorCell(State.Empty, Color.Gray){x = x, y = y};
            }
            map[0, 0] = new ColorCell(State.Three, Color.Red);
            
            map[width - 1, height - 1] = new ColorCell(State.Three, Color.Blue);
                
            return map;
        }
    }
}