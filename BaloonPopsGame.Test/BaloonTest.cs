namespace BaloonsPopGame.Tests
{
    using System;
    using BalloonsPops.Data;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class BaloonTest
    {
        private Color color;
        private Baloon firstBaloon;
        private Baloon secondBaloon;

        [TestInitialize]
        public void Init()
        {
            this.color = new Color(ConsoleColor.Black, 2);
            this.firstBaloon = new Baloon("x", color);
            this.secondBaloon = new Baloon("x", color);
        }


        [TestMethod]
        public void Test_ComapreTwoSameBaloons_ShouldReturnTrue()
        {
            Assert.AreEqual(this.firstBaloon, this.secondBaloon);
        }

        [TestMethod]
        public void Test_CompareTwoDifferentBaloons_ShouldReturnFalse()
        {
            var newBaloon = new Baloon("y", this.color);
            Assert.AreNotEqual(newBaloon, this.firstBaloon);
        }

        [TestMethod]
        public void Test_CompareWithNonBaloonObjet_ShouldReturnFalse()
        {
            Assert.AreNotEqual(this.firstBaloon, this.color);
        }
    }
}
