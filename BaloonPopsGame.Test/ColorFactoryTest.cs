using System;
using BalloonsPops.Core;
using BalloonsPops.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BaloonPopsGame.Test
{
    [TestClass]
    public class ColorFactoryTest
    {
        private Color color;

        [TestInitialize]
        public void G0etColor()
        {
            this.color = ColorFactory.GetRandomColor(1);
        }
        [TestMethod]
        public void Test_GenerateColor_ShouldReturnObjectOfTypeColor()
        {
            Assert.IsInstanceOfType(this.color, typeof(Color));
        }

        [TestMethod]
        public void Test_GenerateColor_ShouldGenerateRedColor()
        {
            Assert.AreEqual(ConsoleColor.Red, this.color.ConsoleColor);
        }
    }
}
