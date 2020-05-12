﻿namespace Survival
{
    public class Bomb : ICell
    {
        // Лучше два следующих метода выделить в отедельный интерфейс, какой-нибудь IDrawingElement
        // Так же эти методы по большому счету используются в представлении, возможно их стоит вынести на уровень представления
        // Draw(ICell cell) => cell is Wall => DrawWall(cell);
        public string GetImageFileName()
        {
            return "Bomb.png";
        }

        public int GetDrawingPriority()
        {
            throw new System.NotImplementedException();
        }

        public void Act(Game game, Player player)
        {
            throw new System.NotImplementedException();
        }

        public void ChangeInConflict(Game game, Player player)
        {
            foreach (var (i, j) in Game.Neighbours)
            {
                if (game.Map[player.X + i, player.Y + j] is Wall) continue;
                game.Map[player.X + i, player.Y + j] = new ColorCell(State.Empty, Color.Gray);
            }
            // В ColorCell можно создать статическое свойство, возвращающее new ColorCell(State.Empty, Color.Grey);
            // Тогда будет удобно делать ColorCell.Grey;
            game.Map[player.X, player.Y] = new ColorCell(State.Empty, Color.Gray);
        }
        
    }
}