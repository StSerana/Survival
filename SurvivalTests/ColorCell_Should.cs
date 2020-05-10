using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Survival;
using FluentAssertions;

namespace SurvivalTests
{
    [TestFixture]
    public class ColorCell_Should
    {
        [Test]
        public static void ChangeOnlyOneCellTest()
        {
            var expectedMap = new List<string>{"WWW","WH2W","WWW"};
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

            Game.Human.X = 1;
            Game.Human.Y = 1;
            Game.Act(Game.Human);
            var actualMap = Game.MapToString().ToList();
            //Assert.IsTrue(Game.MapsAreEqual(expectedMap, actualMap));
            expectedMap.Should().Contain(actualMap);
        }
        
        [Test]
        public static void ChangeMapStateOnFirstTurnTest()
        {
            Game.Start(4,4);
            var expectedMap = new List<string>{"WWWW","WE0A1W","WA1H3W","WWWW"};
            Game.Ai.X = 1;
            Game.Ai.Y = 1;
            Game.Act(Game.Ai);
            var actualMap = Game.MapToString().ToList();
            //Assert.IsTrue(Game.MapsAreEqual(expectedMap, actualMap));
            expectedMap.Should().Contain(actualMap);
        }

        [Test]
        public static void ChangeMapStateOnSecondTurnTest()
        {
            Game.Start(4,4);
            var expectedMap = new List<string>{"WWWW","WE0H2W","WH2E0W","WWWW"};
            Game.Ai.X = 1;
            Game.Ai.Y = 1;
            Game.Act(Game.Ai);
            Game.Human.X = 2;
            Game.Human.Y = 2;
            Game.Act(Game.Human);
            var actualMap = Game.MapToString().ToList();
            //Assert.IsTrue(Game.MapsAreEqual(expectedMap, actualMap));
            expectedMap.Should().Contain(actualMap);
        }
    }
}