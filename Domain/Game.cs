using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Windows.Forms;
using Survival.Views;

namespace Survival
{
    public class Game
    {
        public ICell[,] Map;
        public Player Human;
        public Player Ai;
        public bool IsHumanTurn;
        
        public (bool, Player) IsOver;
        public static readonly (int,int)[] Neighbours = {(0, -1), (0, 1), (-1, 0), (1, 0)};

        public int MapWidth => Map.GetLength(0);
        public int MapHeight => Map.GetLength(1);

        private void CreateMap(int width, int height)
        {
            Map = MapCreator.CreateMap(width, height);
        }

        private void CreatePlayers()
        {
            Human = new Player(Color.Blue);
            Ai = new Ai(Color.Red);
        }

        public void Start(int x, int y)
        {
            CreateMap(x,y);
            CreatePlayers(); 
        }

        public void Act(Player player)
        {
            Map[player.X, player.Y].Act(this, player);
            IsOver = (IsPlayerWinner(player), player);
        }

        public void ChangeMap(Player player)
        {
            Map[player.X, player.Y].ChangeInConflict(this, player);
            IsOver = (IsPlayerWinner(player), player);
        }
        

        public bool IsPlayerWinner(Player player)
        {
            var queue = new Queue<Point>();
            var visited = new HashSet<Point>();
            queue.Enqueue(new Point { X = 0, Y = 0 });
            while (queue.Count != 0)
            {
                var point = queue.Dequeue();
                if (point.X < 0 || point.X >= MapWidth || point.Y < 0 || point.Y >= MapHeight) continue;
                if (visited.Contains(point)) continue;
                if (Map[point.X, point.Y] is ColorCell && ((ColorCell)Map[point.X, point.Y]).Color == Color.Gray) continue;
                if (Map[point.X, point.Y] is ColorCell && ((ColorCell)Map[point.X, point.Y]).Color != player.Color) return false;
                visited.Add(point);
                
                for (var dy = -1; dy <= 1; dy++)
                for (var dx = -1; dx <= 1; dx++)
                    if (dx != dy && dx != -dy) 
                        queue.Enqueue(new Point { X = point.X + dx, Y = point.Y + dy });
            }
            return true;
        }
        
    }
}