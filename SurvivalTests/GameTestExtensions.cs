using System.Collections.Generic;
using System.Linq;
using System.Text;
using Survival;

namespace SurvivalTests
{
    public class GameTestExtensions
    {
        public static IEnumerable<string> MapToString(Game game)
        {
            for (var x = 0; x < game.MapWidth; x++)
            {
                var map = new StringBuilder();
                for (var y = 0; y < game.MapHeight; y++)
                {
                    switch (game.Map[x, y])
                    {
                        case Wall _:
                            map.Append("W");
                            break;
                        case Bomb _:
                            map.Append("B");
                            break;
                        case ColorCell _ when ((ColorCell) game.Map[x, y]).Color == Color.Red:
                            map.Append("A" + (int)((ColorCell) game.Map[x, y]).State);
                            break;
                        case ColorCell _ when ((ColorCell) game.Map[x, y]).Color == Color.Gray:
                            map.Append("E" + (int)((ColorCell) game.Map[x, y]).State);
                            break;
                        default:
                            map.Append("H" + (int)((ColorCell) game.Map[x, y]).State);
                            break;
                    }
                }

                yield return map.ToString();
            }
        }

        public static bool AreEqual(IEnumerable<string> m1, IReadOnlyList<string> m2)
        {
            return !m1.Where((t, i) => !t.Equals(m2[i])).Any();
        }
    }
}