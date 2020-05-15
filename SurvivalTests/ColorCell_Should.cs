using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Survival;
using FluentAssertions;

using static SurvivalTests.GameTestExtensions;

namespace SurvivalTests
{
    [TestFixture]
    public class ColorCell_Should
    {
        [Test]
        public static void ChangeOnlyOneCellTest()
        {
            var game = new Game();
            var expectedMap = new List<string>{"WWW","WH2W","WWW"};
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

            game.Human.X = 1;
            game.Human.Y = 1;
            game.Act(game.Human);
            var actualMap = MapToString(game).ToList();
            expectedMap.Should().Contain(actualMap);
        }
        
        [Test]
        public static void ChangeMapStateOnFirstTurnTest()
        {
            var game = new Game();
            game.Start(4,4);
            var expectedMap = new List<string> {"WWWW", "WA2E0W", "WE0H3W", "WWWW"};
            game.Ai.X = 1;
            game.Ai.Y = 1;
            game.Act(game.Ai);
            var actualMap = MapToString(game).ToList();
            expectedMap.Should().Contain(actualMap);
        }

        [Test]
        public static void ChangeMapStateOnSecondTurnTest()
        {
            var game = new Game();
            game.Start(4,4);
            var expectedMap = new List<string>{"WWWW","WE0H2W","WH2E0W","WWWW"};
            game.Ai.X = 1;
            game.Ai.Y = 1;
            game.Act(game.Ai);
            game.Human.X = 2;
            game.Human.Y = 2;
            game.Act(game.Human);
            var actualMap = MapToString(game).ToList();
            expectedMap.Should().Contain(actualMap);
        }
    }
}