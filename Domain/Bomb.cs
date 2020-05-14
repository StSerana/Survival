namespace Survival
{
    public class Bomb : ICell
    {
        // Лучше два следующих метода выделить в отедельный интерфейс, какой-нибудь IDrawingElement
        // Так же эти методы по большому счету используются в представлении, возможно их стоит вынести на уровень представления
        // Draw(ICell cell) => cell is Wall => DrawWall(cell);

        public void Act(Game game, Player player)
        {
        }

        public void ChangeInConflict(Game game, Player player)
        {
            foreach (var (i, j) in Game.Neighbours)
            {
                if (game.Map[player.X + i, player.Y + j] is Wall) continue;
                game.Map[player.X + i, player.Y + j] = ColorCell.EmptyCell;
            }
            game.Map[player.X, player.Y] = ColorCell.EmptyCell;
        }
        
    }
}