namespace Survival
{
    public static class MapCreator
    {
        public static ICell[,] CreateMap(int width, int height)
        {
            var map = new ICell[width, height];
            for (var dx = 0; dx < width; dx++)
            for (var dy = 0; dy < height; dy++)
            {
                if(dx == 0 || dy == 0 || dx == width - 1 || dy == height - 1)
                    map[dx, dy] = new Wall();
                else
                    map[dx, dy] = new ColorCell(State.Empty, Color.Gray);
            }
            map[1, 1] = new ColorCell(State.Three, Color.Red);
            
            map[width - 2, height - 2] = new ColorCell(State.Three, Color.Blue);
                
            return map;
        }
    }
}