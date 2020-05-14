using System;

namespace Survival
{
    public class Ai : Player
    {
        private static readonly Random Random = new Random();
        public Ai(Color color) : base(color)
        {
        }

        public void GenerateRandomTurn(Game game)
        {
            var dx = Random.Next(1, game.MapWidth - 1);
            var dy = Random.Next(1, game.MapHeight - 1);
            if (game.Map[dx, dy] is ColorCell && ((ColorCell) game.Map[dx, dy]).Color == Color.Red)
            {
                X = dx;
                Y = dy;
            }
            else 
                GenerateRandomTurn(game);
        }
    }
}