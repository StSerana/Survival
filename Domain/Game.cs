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
        // сеттеры закрыть, >>статику убрать<< (кроме Neighbours и т.п.)
        public ICell[,] Map;
        public Player Human;
        public Player Ai;
        public bool HumanTurn = true;
        
        public bool IsOver;
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
            Ai = new AI(Color.Red);
        }

        public void Start(int x, int y)
        {
            CreateMap(x,y);
            CreatePlayers();
            //здесь идет смена стадии старта на стадию непосредственно игры  
        }

        public void Act(Player player)
        {
            Map[player.X, player.Y].Act(this, player);
        }

        public void ChangeMap(Player player)
        {
            Map[player.X, player.Y].ChangeInConflict(this, player);
        }
        

        public bool IsHumanWinner()
        {
            var queue = new Queue<Point>();
            var visited = new HashSet<Point>();
            queue.Enqueue(new Point { X = 0, Y = 0 });
            while (queue.Count != 0)
            {
                var point = queue.Dequeue();
                if (point.X < 0 || point.X >= MapWidth || point.Y < 0 || point.Y >= MapHeight) continue;
                if (visited.Contains(point)) continue;
                // лучше название цвета заменить на говорящее значение, а потом при отрисовке в соответствии с состоянием выбирать цвет
                if (Map[point.X, point.Y] is ColorCell && ((ColorCell)Map[point.X, point.Y]).Color != Color.Blue) return false;
                visited.Add(point);
                
                for (var dy = -1; dy <= 1; dy++)
                for (var dx = -1; dx <= 1; dx++)
                    if (dx != 0 && dy != 0) continue;
                    else queue.Enqueue(new Point { X = point.X + dx, Y = point.Y + dy });

            }

            return true;
        }
        // Если это только в тестах используется, то лучше сделать какой-нибудь GameTestExtensions и туда это засунуть
        public IEnumerable<string> MapToString()
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
                        case ColorCell _ when ((ColorCell) Map[x, y]).Color == Color.Red:
                            map.Append("A" + (int)((ColorCell) Map[x, y]).State);
                            break;
                        case ColorCell _ when ((ColorCell) Map[x, y]).Color == Color.Gray:
                            map.Append("E" + (int)((ColorCell) Map[x, y]).State);
                            break;
                        default:
                            map.Append("H" + (int)((ColorCell) Map[x, y]).State);
                            break;
                    }
                }

                yield return map.ToString();
            }
        }

        // Глагол в начале названия метода. Снова метод, актуальный только для тестирования
        public static bool AreEqual(IEnumerable<string> m1, IReadOnlyList<string> m2)
        {
            // Убрать отрицаение и вместо Any сделать All
            return !m1.Where((t, i) => !t.Equals(m2[i])).Any();
        }
    }
}