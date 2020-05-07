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

        public CreatureCommand Act(int x, int y, Player player)
        {
            return new CreatureCommand();
        }

        public void ChangeInConflict(int x, int y, ICell cell)
        {
            for(var i = -1; i <= 1; i++)
            for (var j = -1; j <= 1; j++)
                if (i != j && i != -j)
                    Game.Map[x + i, y + j] = new ColorCell(State.Empty, Color.Gray);
        }
    }
}