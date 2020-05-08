namespace Survival
{
    public class ColorCell : ICell
    {
        public Color color;
        public State state;

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

        public void Act(Player player)
        {
            if (color != player.Color) return;
            switch (state)
            {
                case State.One:
                case State.Two:
                    Game.Map[player.X, player.Y] = new ColorCell(state + 1, color);
                    break;
                case State.Three:
                {
                    foreach (var (item1, item2) in Game.Neighbours)
                    {
                        var current = new Player(player.Color){X = player.X, Y = player.Y};
                        current.X += item1;
                        current.Y += item2;
                        Game.ChangeMap(current);
                    }
                    Game.Map[player.X, player.Y] = new ColorCell(State.Empty, Color.Gray);
                    break;
                }
            }
        }

        public void ChangeInConflict(Player player)
        {
            if(state != State.Three)
                Game.Map[player.X, player.Y] = new ColorCell(state + 1, player.Color);
            else
            {
                foreach (var (item1, item2) in Game.Neighbours)
                {
                    var current = player;
                    current.X += item1;
                    current.Y += item2;
                    Game.ChangeMap(current);
                }
                Game.Map[player.X, player.Y] = new ColorCell(State.Empty, Color.Gray);
            }
                
        }
    }
}