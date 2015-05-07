using System;
using System.Collections.Generic;
using BalloonsPops;
using BalloonsPops.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BaloonPopsGame.Test
{
    [TestClass]
    public class TopPlayersTests
    {
        [TestMethod]
        public void Test_AddingScoreToPlayersScores_ShouldAddCorrectData()
        {
            var topPlayers = TopPlayers.Instance;
            var dataRepository = new DataRepository();
            topPlayers.Load(dataRepository);
            topPlayers.PlayersMoves = new Dictionary<string, int>();
            topPlayers.AddScore("pesho", 123);

            Assert.AreEqual(123, topPlayers.PlayersMoves["pesho"]);
        }

        [TestMethod]
        public void Test_AddingScoreToExistingPlayersScores_ShouldAddCorrectData()
        {
            var topPlayers = TopPlayers.Instance;
            var dataRepository = new DataRepository();
            topPlayers.Load(dataRepository);
            topPlayers.PlayersMoves = new Dictionary<string, int>();
            topPlayers.AddScore("gosho", 123);
            topPlayers.AddScore("gosho", 321);

            Assert.AreEqual(123, topPlayers.PlayersMoves["gosho"]);
            topPlayers.AddScore("gosho", 20);
            Assert.AreEqual(20, topPlayers.PlayersMoves["gosho"]);
        }

        [TestMethod]
        public void Test_IsTopResult_ShouldReturnCorrectData()
        {
            var topPlayers = TopPlayers.Instance;
            var dataRepository = new DataRepository();
            topPlayers.Load(dataRepository);
            topPlayers.PlayersMoves = new Dictionary<string, int>();
            topPlayers.PlayersMoves.Add("pesho", 5);
            topPlayers.PlayersMoves.Add("gosho", 10);
            topPlayers.PlayersMoves.Add("ivna", 11);
            topPlayers.PlayersMoves.Add("petkja", 12);
            topPlayers.PlayersMoves.Add("dimo", 14);

            var resultTrue = topPlayers.IsTopResult(12);
            Assert.IsTrue(resultTrue);
            var resultFalse = topPlayers.IsTopResult(20);
            Assert.IsFalse(resultFalse);
        }
    }
}
