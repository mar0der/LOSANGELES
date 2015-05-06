using System;
using System.CodeDom;
using BalloonsPops.Core;
using BalloonsPops.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BaloonPopsGame.Test
{
    [TestClass]
    public class GameBoardGeneratorTest
    {
        [TestMethod]
        public void Test_GameBoardGenerator_ShouldFillAllFieldsWithBaloons()
        {
            var gameBoard = GameBoardGenerator.GenerateGameBoard(20, 20, 4);
            for (int row = 0; row < gameBoard.GetLength(0); row++)
            {
                for (int col = 0; col < gameBoard.GetLength(1); col++)
                {
                    Assert.IsInstanceOfType(gameBoard[row, col], typeof(Baloon));
                }
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void Test_GameBoardGeneratorWithIvalidParameters_ShouldThrowAnExeption()
        {
            GameBoardGenerator.GenerateGameBoard(-20, 20, 4);

        }
    }
}
