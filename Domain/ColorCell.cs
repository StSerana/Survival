namespace Survival
{
    public class ColorCell : ICell
    {
        // оба поля превратить в свойства с закрытыми сеттерами
        public Color Color { get; }
        public State State { get; }

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

        public int GetDrawingPriority()
        {
            return 1;
        }

        public void Act(Player player)
        {
            if (Color != player.Color) return;
            switch (State)
            {
                case State.One:
                case State.Two:
                    Game.Map[player.X, player.Y] = new ColorCell(State + 1, Color);
                    break;
                case State.Three:
                {
                    // переименовать item1 и item2 в deltaX и deltaY
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
            if (State != State.Three)
                Game.Map[player.X, player.Y] = new ColorCell(State + 1, player.Color);
            else
            {
                // повторение кода выше
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