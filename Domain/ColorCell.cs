namespace Survival
{
    public class ColorCell : ICell
    {
        // оба поля превратить в свойства с закрытыми сеттерами
        public Color Color { get; }
        public State State { get; }
        
        public static readonly ColorCell EmptyCell = new ColorCell(State.Empty, Color.Gray);

        public ColorCell(State state, Color color)
        {
            Color = color;
            State = state;
        }
        
        public string GetImageFileName()
        {
            return (state: State, color: Color) switch
            {
                (State.One, Color.Blue) => "BlueOne.png",
                (State.Two, Color.Blue) => "BlueTwo.png",
                (State.Three, Color.Blue) => "BlueThree.png",
                (State.One, Color.Red) => "RedOne.png",
                (State.Two, Color.Red) => "RedTwo.png",
                (State.Three, Color.Red) => "RedThree.png",
                _ => "EmptyCell.png"
            };
        }

        public void Act(Game game, Player player)
        {
            if (Color != player.Color) return;
            switch (State)
            {
                case State.One:
                case State.Two:
                    game.Map[player.X, player.Y] = new ColorCell(State + 1, Color);
                    break;
                case State.Three:
                {
                    foreach (var (dx, dy) in Game.Neighbours)
                    {
                        var current = new Player(player.Color){X = player.X, Y = player.Y};
                        current.X += dx;
                        current.Y += dy;
                        game.ChangeMap(current);
                    }
                    game.Map[player.X, player.Y] = EmptyCell;
                    break;
                }
            }
        }

        public void ChangeInConflict(Game game, Player player)
        {
            if (State != State.Three)
                game.Map[player.X, player.Y] = new ColorCell(State + 1, player.Color);
            else
            {
                game.Map[player.X, player.Y] = EmptyCell;
                foreach (var (dx, dy) in Game.Neighbours)
                {
                    var current = player;
                    current.X += dx;
                    current.Y += dy;
                    game.ChangeMap(current);
                }
                
            }
                
        }
    }
}