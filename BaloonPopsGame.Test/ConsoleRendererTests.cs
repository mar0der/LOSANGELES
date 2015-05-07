using System;
using System.Collections.Generic;
using System.IO;
using BalloonsPops.Data;
using BalloonsPops.Interfaces;
using BalloonsPops.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BaloonPopsGame.Test
{
    [TestClass]
    public class ConsoleRendererTests
    {
        private StreamWriter sw;
        private string fileName;
        private ConsoleRenderer consoleRenderer;

        [TestInitialize]
        public void TestInitialize()
        {
            this.consoleRenderer = new ConsoleRenderer();
            this.fileName = "consoleRendererOutput.txt";
            this.sw = new StreamWriter(fileName);
            this.sw.AutoFlush = true;
            Console.SetOut(this.sw);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            this.sw.Close();
        }

        [TestMethod]
        public void Test_IsRendererPrintsTheGameFieldCorrectly_ShouldMatchTheStrings()
        {

            IEntity[,] entities = new IEntity[1, 1];
            Baloon baloon = new Baloon("$", new Color(ConsoleColor.White, 0));
            entities[0, 0] = baloon;
            var gameBoard = new GameBoard(entities);
            this.consoleRenderer.RenderGameBoard(gameBoard);
            this.sw.Close();
            using (StreamReader sr = new StreamReader(this.fileName))
            {
                var output = sr.ReadLine();
                Assert.AreEqual("    0 ", output);
                output = sr.ReadLine();
                Assert.AreEqual("    - ", output);
                output = sr.ReadLine();
                Assert.AreEqual("0 | $ ", output);
            }
        }

        [TestMethod]
        public void Test_StaticTest_ShouldPrintTheStaticTest()
        {
            string text = "abc";

            this.consoleRenderer.PrintStaticText(text);
            this.sw.Close();
            using (StreamReader sr = new StreamReader(this.fileName))
            {
                var output = sr.ReadLine();
                Assert.AreEqual(text, output);
            }
        }

        [TestMethod]
        public void Test_TopPLayersList_ShouldPrintTopPlayerList()
        {
            string text = "abc";
            var players = new Dictionary<string, int>();
            players.Add("pesho", 12);
            players.Add("dimo", 5);
            this.consoleRenderer.PrintTopPlayers(players);
            this.sw.Close();
            using (StreamReader sr = new StreamReader(this.fileName))
            {
                var line1 = sr.ReadLine();
                Assert.AreEqual("Scoreboard:", line1);
                var line2 = sr.ReadLine();
                Assert.AreEqual("1. pesho --> 12", line2);
                var line3 = sr.ReadLine();
                Assert.AreEqual("2. dimo --> 5", line3);
            }
        }

        [TestMethod]
        public void Test_TopPlayersList_ShoudReturnCorrectText()
        {
            this.consoleRenderer.PrintTopPlayers(new Dictionary<string, int>());
            sw.Close();
            using (StreamReader sr = new StreamReader(this.fileName))
            {
                var line1 = sr.ReadLine();
                Assert.AreEqual("Scoreboard:", line1);
                var line2 = sr.ReadLine();
                Assert.AreEqual("No saved results.", line2);
            }
        }

        [TestMethod]
        public void Test_CurrentScore_ShouldReturnCurrentMoves()
        {
            this.consoleRenderer.PrintCurrentScore(10);
            sw.Close();
            using (StreamReader sr = new StreamReader(this.fileName))
            {
                var line1 = sr.ReadLine();
                Assert.AreEqual("Your moves: 10", line1);
            }
        }

    }
}
