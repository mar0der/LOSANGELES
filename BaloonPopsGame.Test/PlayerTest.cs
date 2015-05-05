

namespace BaloonsPopGame.Tests
{
    using BalloonsPops.Data;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    [TestClass]
    public class PlayerTest
    {
        [TestMethod]
        public void Test_CreateValidPlayer_ShouldHaveCorrectValues()
        {
            var player = new Player("pesho");
            Assert.AreEqual("pesho", player.Name);
            Assert.AreEqual(0, player.CurrentMoves);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "The name must be at least 2 characters")]
        public void Test_SetInvalidUsername_ShouldThrowExeption()
        {
            var player = new Player("g");
        }
    }
}
