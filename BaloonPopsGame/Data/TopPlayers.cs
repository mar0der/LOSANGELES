namespace BalloonsPops.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using Interfaces;

    public sealed class TopPlayers
    {
        private static volatile TopPlayers instance;

        private TopPlayers()
        {
        }

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

        public IDataRepository DataRepository { get; set; }

        public Dictionary<string, int> PlayersMoves { get; set; }

        public void Load(IDataRepository dataRepository)
        {
            this.DataRepository = dataRepository;
            this.PlayersMoves = this.DataRepository.Load(Config.TopPlayerFile);
        }

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
