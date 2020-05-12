using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Survival;
using FluentAssertions;
//дописать fluent

namespace SurvivalTests
{
    [TestFixture]
    public class Map_Should
    {
        [Test]
        public void RightStartMapCreationTest()
        {
            var game = new Game();
            game.Start(4, 4);
            var expectedMap = new List<string>{"WWWW","WA3E0W","WE0H3W","WWWW"};
            var actualMap = game.MapToString().ToList();
            Assert.IsTrue(Game.AreEqual(expectedMap, actualMap));
        }

        [Test]
        public void CheckHumanHasWon()
        {
            var game = new Game();
            game.Start(4, 4);
            game.Map = new ICell[3, 3];
            for (var x = 0; x < 3; x++)
            for (var y = 0; y < 3; y++)
            {
                if(x == 0 || y == 0 || x == 2 || y == 2)
                    game.Map[x, y] = new Wall();
                else
                    game.Map[x, y] = new ColorCell(State.One, Color.Blue);
            }
            Assert.IsTrue(game.IsHumanWinner());
        }
        
        [Test]
        public void CheckHumanHasNotWonOnStart()
        {
            var game = new Game();
            game.Start(4, 4);
            foreach (var line in game.MapToString()) Console.WriteLine(line);
            var answer = game.IsHumanWinner();
            Assert.IsFalse(answer);
        }
        //лишний отступ
    }
}