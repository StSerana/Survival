using System;
using System.Collections.Generic;
using System.Drawing;
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

        public static int MapWidth => Map.GetLength(0);
        public static int MapHeight => Map.GetLength(1);

        private static void CreateMap(int width, int height)
        {
            Map = MapCreator.CreateMap(width, height);
        }

        private static void CreatePlayers()
        {
            Human = new Player(Color.Blue);
            Ai = new AI(Color.Red);
        }

        public void Start()
        {
            CreateMap(4,4);
            CreatePlayers();
            
        }

        private bool IsHumanWinner()
        {
            var queue = new Queue<Point>();
            var visited = new HashSet<Point>();
            visited.Add(new Point {X = 0, Y = 0});
            queue.Enqueue(new Point { X = 0, Y = 0 });
            while (queue.Count != 0)
            {
                var point = queue.Dequeue();
                if (point.X < 0 || point.X >= Game.MapWidth || point.Y < 0 || point.Y >= Game.MapHeight) continue;
                if (visited.Contains(point)) continue;
                if (Map[point.X, point.Y] is ColorCell && ((ColorCell)Map[point.X, point.Y]).color == Color.Red) return false;
                visited.Add(point);
                
                for (var dy = -1; dy <= 1; dy++)
                for (var dx = -1; dx <= 1; dx++)
                    if (dx != 0 && dy != 0) continue;
                    else queue.Enqueue(new Point { X = point.X + dx, Y = point.Y + dy });

            }

            return true;
        }

        public string MapToString()
        {
            var map = new StringBuilder();
            for (var x = 0; x < MapWidth; x++)
            {
                for (var y = 0; y < MapHeight; y++)
                {
                    if (Map[x, y] is Wall)
                        map.Append("W");
                    if (Map[x, y] is Bomb)
                        map.Append("B");
                    if (Map[x, y] is ColorCell && ((ColorCell) Map[x, y]).color == Color.Red)
                        map.Append("A" + ((ColorCell) Map[x, y]).state);
                    else
                        map.Append("H" + ((ColorCell) Map[x, y]).state);
                }
                map.Append("\n");
            }

            return map.ToString();
        }
        
    }
}