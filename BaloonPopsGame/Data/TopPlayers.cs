using BalloonsPops.Interfaces;

namespace BalloonsPops.Data
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    public sealed class TopPlayers
    {
        private static object syncLock = new object();
        private static volatile TopPlayers instance;

        private TopPlayers()
        {
        }

        private IDataRepository DataRepository { get; set; }


        public static TopPlayers Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new TopPlayers();
                }

                return instance;
            }
        }

        public void Load(IDataRepository dataRepository)
        {
            this.DataRepository = dataRepository;
            this.PlayersMoves = this.DataRepository.Load(Config.TopPlayerFile);
        }

        public Dictionary<string, int> PlayersMoves { get; set; }

        public void AddScore(string name, int moves)
        {
            if (this.PlayersMoves.ContainsKey(name))
            {
                if (this.PlayersMoves[name] > moves)
                {
                    this.PlayersMoves[name] = moves;
                }
            }
            else
            {
                this.PlayersMoves.Add(name, moves);
            }

            this.PlayersMoves = this.PlayersMoves
                .OrderBy(s => s.Value)
                .Take(5)
                .ToDictionary(s => s.Key, s => s.Value);

        }

        public bool IsTopResult(int moves)
        {
            var betterUsers = this.PlayersMoves.Where(p => p.Value <= moves).Count();

            return betterUsers < Config.NumberOfTopResutsShown;
        }
    }
}
