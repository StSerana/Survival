namespace Survival
{
    public class ColorCell : ICell
    {
        public readonly Color color;
        public readonly State state;
        public int x;
        public int y;

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
            return 1;
        }

        public CreatureCommand Act(Player player)
        {
            if(color != player.Color) return new CreatureCommand();
            switch (state)
            {
                case State.One:
                case State.Two:
                    return new CreatureCommand{TransformTo = new ColorCell(state + 1, color)};
                case State.Three:
                {
                    foreach (var (item1, item2) in Game.Neighbours)
                        Game.Map[x + item1, y + item2].ChangeInConflict(this); 
                    Game.Map[x, y] = new ColorCell(State.Empty, Color.Gray){x = x, y = y};
                    break;
                }
            }

            return new CreatureCommand{TransformTo = new ColorCell(State.Empty, Color.Gray){x = x, y = y}};
        }

        public void ChangeInConflict(ICell conflictedObject)
        {
            if (!(conflictedObject is ColorCell)) return;
            if(state != State.Three)
                Game.Map[x, y] = new ColorCell(state + 1, ((ColorCell) conflictedObject).color){x = x, y = y};
            else 
                foreach (var (item1, item2) in Game.Neighbours)
                {
                    Game.Map[x + item1, y + item2].ChangeInConflict(this);
                }
        }
    }
}