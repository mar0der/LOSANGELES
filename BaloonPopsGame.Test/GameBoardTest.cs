using BalloonsPops.Data;
using BalloonsPops.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace BaloonsPopGame.Tests
{
    [TestClass]
    public class GameBoardTest
    {

        [TestMethod]
        public void Test_MoveElementDown_SholdReturnTrue()
        {
            Color color = new Color(ConsoleColor.Black, 1);
            IEntity fullEntity = new Baloon("1", color);
            IEntity emptyEntity = new Baloon(".", color);
            IEntity[,] entities = new Baloon[2, 2];

            entities[0, 0] = fullEntity;
            entities[0, 1] = fullEntity;
            entities[1, 0] = emptyEntity;
            entities[1, 1] = fullEntity;

            GameBoard gameBoard = new GameBoard(entities);
            gameBoard.Drop();

            Assert.AreEqual(".", gameBoard.Entities[0, 0].Symbol);
            Assert.AreEqual("1", gameBoard.Entities[0, 1].Symbol);
            Assert.AreEqual("1", gameBoard.Entities[1, 0].Symbol);
            Assert.AreEqual("1", gameBoard.Entities[1, 1].Symbol);
        }
    }
}
