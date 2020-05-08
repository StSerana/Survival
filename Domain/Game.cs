using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Survival.Views;

namespace Survival
{
    public class Game
    {
        public static ICell[,] Map;
        public static Player Human;
        public static Player Ai;
        public static bool HumanTurn = true;
        
        public static bool IsOver;
        public static readonly (int,int)[] Neighbours = {(0, -1), (0, 1), (-1, 0), (1, 0)};

        private static int MapWidth => Map.GetLength(0);
        private static int MapHeight => Map.GetLength(1);

        private static void CreateMap(int width, int height)
        {
            Map = MapCreator.CreateMap(width, height);
        }

        private static void CreatePlayers()
        {
            Human = new Player(Color.Blue);
            Ai = new AI(Color.Red);
        }

        public static void Start(int x, int y)
        {
            CreateMap(x,y);
            CreatePlayers();
            //здесь идет смена стадии старта на стадию непосредственно игры  
        }

        public static void Act(Player player)
        {
            Map[player.X, player.Y].Act(player);
        }

        public static void ChangeMap(Player player)
        {
            Map[player.X, player.Y].ChangeInConflict(player);
        }
        

        public static bool IsHumanWinner()
        {
            var queue = new Queue<Point>();
            var visited = new HashSet<Point>();
            queue.Enqueue(new Point { X = 0, Y = 0 });
            while (queue.Count != 0)
            {
                var point = queue.Dequeue();
                if (point.X < 0 || point.X >= MapWidth || point.Y < 0 || point.Y >= MapHeight) continue;
                if (visited.Contains(point)) continue;
                if (Map[point.X, point.Y] is ColorCell && ((ColorCell)Map[point.X, point.Y]).color != Color.Blue) return false;
                visited.Add(point);
                
                for (var dy = -1; dy <= 1; dy++)
                for (var dx = -1; dx <= 1; dx++)
                    if (dx != 0 && dy != 0) continue;
                    else queue.Enqueue(new Point { X = point.X + dx, Y = point.Y + dy });

            }

            return true;
        }

        public static IEnumerable<string> MapToString()
        {
            for (var x = 0; x < MapWidth; x++)
            {
                var map = new StringBuilder();
                for (var y = 0; y < MapHeight; y++)
                {
                    switch (Map[x, y])
                    {
                        case Wall _:
                            map.Append("W");
                            break;
                        case Bomb _:
                            map.Append("B");
                            break;
                        case ColorCell _ when ((ColorCell) Map[x, y]).color == Color.Red:
                            map.Append("A" + (int)((ColorCell) Map[x, y]).state);
                            break;
                        case ColorCell _ when ((ColorCell) Map[x, y]).color == Color.Gray:
                            map.Append("E" + (int)((ColorCell) Map[x, y]).state);
                            break;
                        default:
                            map.Append("H" + (int)((ColorCell) Map[x, y]).state);
                            break;
                    }
                }

                yield return map.ToString();
            }
        }

        public static bool MapsAreEqual(IEnumerable<string> m1, IReadOnlyList<string> m2)
        {
            return !m1.Where((t, i) => !t.Equals(m2[i])).Any();
        }
    }
}