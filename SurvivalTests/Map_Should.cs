using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using Survival;
using static SurvivalTests.GameTestExtensions;

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
            var actualMap = MapToString(game).ToList();
            expectedMap.Should().Contain(actualMap);
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
            Assert.IsTrue(game.IsPlayerWinner(game.Human));
        }
        
        [Test]
        public void CheckHumanHasNotWonOnStart()
        {
            var game = new Game();
            game.Start(4, 4);
            var answer = game.IsPlayerWinner(game.Ai);
            Assert.IsFalse(answer);
        }
    }
}