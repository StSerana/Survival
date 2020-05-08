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
                Game.Map[player.X + i, player.Y + j] = new ColorCell(State.Empty, Color.Gray);
            }
        }
        
    }
}