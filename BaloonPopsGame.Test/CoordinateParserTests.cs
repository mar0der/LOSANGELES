using System;
using BalloonsPops.Exceptions;
using BalloonsPops.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BaloonPopsGame.Test
{
    [TestClass]
    public class CoordinateParserTests
    {
        [TestMethod]
        public void Test_ParsingCordinates_ShouldReturnArrayOfCordinates()
        {
            var coordinates = CoordinateParser.Parse("4 8");
            Assert.AreEqual(4, coordinates[0]);
            Assert.AreEqual(8, coordinates[1]);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidCommand))]
        public void Test_ParsingCordinates_ShouldThrowInvalidCommand()
        {
            var coordinates = CoordinateParser.Parse("12");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidCoordinates))]
        public void Test_ParsingOutsideOfGameBoardCordinates_ShouldThrowInvalidCoordinates()
        {
            var coordinates = CoordinateParser.Parse("22 20");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidCoordinates))]
        public void Test_ParsingWrongCordinates_ShouldThrowInvalidCoordinates()
        {
            var coordinates = CoordinateParser.Parse("asd qwe");
        }
    }
}
