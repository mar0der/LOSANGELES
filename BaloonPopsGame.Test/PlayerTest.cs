

namespace BaloonsPopGame.Tests
{
    using BalloonsPops.Data;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    [TestClass]
    public class PlayerTest
    {
        private Player player;

        [TestInitialize]
        public void Init()
        {
            this.player = new Player("pesho");
        }

        [TestMethod]
        public void Test_CreateValidPlayer_ShouldHaveCorrectValues()
        {
            Assert.AreEqual("pesho", this.player.Name);
            Assert.AreEqual(0, player.CurrentMoves);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "The name must be at least 2 characters")]
        public void Test_SetInvalidUsername_ShouldThrowExeption()
        {
            this.player = new Player("g");
        }
    }
}
