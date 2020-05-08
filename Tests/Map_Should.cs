using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Survival.Tests
{
    [TestFixture]
    public class Map_Should
    {
        [Test]
        public void RightStartMapCreationTest()
        {
            Game.Start(4, 4);
            var expectedMap = new List<string>{"WWWW","WA3E0W","WE0H3W","WWWW"};
            var actualMap = Game.MapToString().ToList();
            Assert.IsTrue(Game.MapsAreEqual(expectedMap, actualMap));
        }

        [Test]
        public void CheckHumanHasWon()
        {
            Game.Start(4, 4);
            Game.Map = new ICell[3, 3];
            for (var x = 0; x < 3; x++)
            for (var y = 0; y < 3; y++)
            {
                if(x == 0 || y == 0 || x == 2 || y == 2)
                    Game.Map[x, y] = new Wall();
                else
                    Game.Map[x, y] = new ColorCell(State.One, Color.Blue);
            }
            Assert.IsTrue(Game.IsHumanWinner());
        }
        
        [Test]
        public void CheckHumanHasNotWonOnStart()
        {
            Game.Start(4, 4);
            foreach (var line in Game.MapToString()) Console.WriteLine(line);
            var answer = Game.IsHumanWinner();
            Assert.IsFalse(answer);
        }

    }
}