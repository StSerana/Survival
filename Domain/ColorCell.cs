namespace Survival
{
    public class ColorCell : ICell
    {
        private Color color;
        private State state;

        public ColorCell(State state, Color color)
        {
            this.color = color;
            this.state = state;
        }
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
            var cell = (ColorCell)Game.Map[x, y];
            if (cell.color != player.Color) return null;
            switch (cell.state)
            {
                case State.One:
                case State.Two:
                    return new CreatureCommand{TransformTo = new ColorCell(cell.state + 1, cell.color)};
                case State.Three:
                {
                    for(var i = -1; i <= 1; i++)
                    for (var j = -1; j <= 1; j++)
                        if (i != j && i != -j)
                            Game.Map[x + i, y + j].ChangeInConflict(x + i, y + j, cell);

                    break;
                }
            }

            return new CreatureCommand{TransformTo = new ColorCell(State.Empty, Color.Gray)};
        }

        public void ChangeInConflict(int x, int y, ICell cell)
        {
            var currentCell = (ColorCell) Game.Map[x, y];
            if(currentCell.state != State.Three) 
                Game.Map[x, y] = new ColorCell(currentCell.state + 1, ((ColorCell)cell).color);
            else
            {
                for(var i = -1; i <= 1; i++)
                for (var j = -1; j <= 1; j++)
                    if (i != j && i != -j)
                        Game.Map[x + i, y + j].ChangeInConflict(x + i, y + j, cell);
            }
        }
    }
}