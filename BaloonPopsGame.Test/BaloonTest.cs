namespace BaloonsPopGame.Tests
{
    using System;
    using BalloonsPops.Data;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    [TestClass]
    public class BaloonTest
    {
       [TestMethod]
        public void Test_ComapreTwoSameBaloons_ShouldReturnTrue()
        {
            var color1 = new Color(ConsoleColor.Black, 2);
            var baloon1 = new Baloon("x", color1);
            var baloon2 = new Baloon("x", color1);
            Assert.AreEqual(baloon1, baloon2);
        }

        [TestMethod]
        public void Test_CompareTwoDifferentBaloons_ShouldReturnFalse()
        {
            var color1 = new Color(ConsoleColor.Black, 2);
            var baloon1 = new Baloon("x", color1);
            var baloon2 = new Baloon("y", color1);
            Assert.AreNotEqual(baloon1, baloon2);
        }

        [TestMethod]
        public void Test_CompareWithNonBaloonObjet_ShouldReturnFalse()
        {
            var color1 = new Color(ConsoleColor.Black, 2);
            var baloon1 = new Baloon("y", color1);
            Assert.AreNotEqual(baloon1, color1);
        }
    }
}
