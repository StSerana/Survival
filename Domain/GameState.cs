namespace Survival
{
    public class GameState
    {
        public void BeginAct()
        {
            for (var x = 0; x < Game.MapWidth; x++)
            for (var y = 0; y < Game.MapHeight; y++)
            {
                var creature = Game.Map[x, y];
            }
        }
    }
}