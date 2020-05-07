using System;
using NUnit.Framework;
using Survival;

namespace Survival.Tests
{
    [TestFixture]
    public class MapTests
    {
        [Test]
        public void CheckHumanHasWon()
        {
            var game = new Game();
            Console.WriteLine(game.MapToString());
        }
           
    }
}