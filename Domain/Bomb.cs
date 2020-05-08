namespace Survival
{
    class Bomb : ICell
    {
        public string GetImageFileName()
        {
            throw new System.NotImplementedException();
        }

        public int GetDrawingPriority()
        {
            throw new System.NotImplementedException();
        }

        public void Act(Player player)
        {
            throw new System.NotImplementedException();
        }

        public void ChangeInConflict(Player player)
        {
            foreach (var (i, j) in Game.Neighbours)
            {
                if (Game.Map[player.X + i, player.Y + j] is Wall) continue;
                Game.Map[player.X + i, player.Y + j] = new ColorCell(State.Empty, Color.Gray);
            }
            Game.Map[player.X, player.Y] = new ColorCell(State.Empty, Color.Gray);
        }
        
    }
}