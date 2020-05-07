﻿namespace Survival
{
    class Bomb : ICell
    {
        public readonly int x;
        public readonly int y;
        public string GetImageFileName()
        {
            throw new System.NotImplementedException();
        }

        public int GetDrawingPriority()
        {
            throw new System.NotImplementedException();
        }

        public CreatureCommand Act(Player player)
        {
            return new CreatureCommand();
        }

        public void ChangeInConflict(ICell conflictedObject)
        {
            foreach (var (i, j) in Game.Neighbours)
            {
                Game.Map[x + i, y + j] = new ColorCell(State.Empty, Color.Gray);
            }
        }
        
    }
}