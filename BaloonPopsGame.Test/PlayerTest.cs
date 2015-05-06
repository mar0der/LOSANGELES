

namespace BaloonsPopGame.Tests
{
    using BalloonsPops.Data;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using BalloonsPops.Exceptions;

    [TestClass]
    public class PlayerTest
    {
        private Player player;

        [TestInitialize]
        public void Init()
        {
            this.player = new Player();
        }

        [TestMethod]
        public void Test_CreateValidPlayer_ShouldHaveCorrectValues()
        {
            this.player.Name = "pesho";
            Assert.AreEqual("pesho", this.player.Name);
            Assert.AreEqual(0, player.CurrentMoves);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidCommand), "The name must be at least 2 characters")]
        public void Test_SetShordUsername_ShouldThrowExeption()
        {
            this.player.Name = "";
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidCommand), "The name must be at least 2 characters")]
        public void Test_SetInvaidCharacters_ShouldThrowExeption()
        {
            this.player.Name = "#$@";
        }
    }
}
