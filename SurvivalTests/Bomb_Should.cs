using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using Survival;
using FluentAssertions;
using static SurvivalTests.GameTestExtensions;

namespace SurvivalTests
{
    [TestFixture]
    public class Bomb_Should
    {
        [Test]
        public static void ClearCellsInTheCenterOfMapTest()
        {
            var game = new Game();
            var expectedMap = new List<string>{"WWWWW", "WH1E0H1W","WE0E0E0W","WH2E0H2W","WWWWW"};
            game.Start(5,5);
            for (var x = 0; x < 5; x++)
            for (var y = 0; y < 5; y++)
            {
                if(x == 0 || y == 0 || x == 4 || y == 4)
                    game.Map[x, y] = new Wall();
                else
                    game.Map[x, y] = new ColorCell(State.One, Color.Blue);
            }
            game.Map[2, 2] = new Bomb();
            game.Map[3, 2] = new ColorCell(State.Three, Color.Blue);
            var player = new Player(Color.Blue){X = 3, Y = 2};
            game.Act(player);
            var actualMap = MapToString(game).ToList();
            expectedMap.Should().Contain(actualMap);
        }

        [Test]
        public static void ClearCellNearTheWallsTest()
        {
            var game = new Game();
            var expectedMap = new List<string>{"WWWWW", "WE0E0H2W","WE0H2H1W","WH1H1H1W","WWWWW"};
            game.Start(5,5);
            for (var x = 0; x < 5; x++)
            for (var y = 0; y < 5; y++)
            {
                if(x == 0 || y == 0 || x == 4 || y == 4)
                    game.Map[x, y] = new Wall();
                else
                    game.Map[x, y] = new ColorCell(State.One, Color.Blue);
            }
            game.Map[1, 1] = new Bomb();
            game.Map[1, 2] = new ColorCell(State.Three, Color.Blue);
            var player = new Player(Color.Blue){X = 1, Y = 2};
            game.Act(player);
            var actualMap = MapToString(game).ToList();
            expectedMap.Should().Contain(actualMap);
        }
    }
}