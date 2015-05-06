using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Remoting.Messaging;
using System.Linq;

namespace BalloonsPops.Data
{
    public sealed class TopPlayers
    {
        private static object syncLock = new object();
        private static volatile TopPlayers instance;

        private TopPlayers()
        {
            this.PlayersMoves = new Dictionary<string, int>();
            this.LoadDataFromFile();
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

            this.SaveDataToFile();
        }

        public Dictionary<string, int> GetHighScore()
        {
            return this.PlayersMoves;
        } 

        private void SaveDataToFile()
        {
            using (StreamWriter writer = new StreamWriter(Config.topPlayerFile))
            {
                string line;
                foreach (KeyValuePair<string, int> score in this.PlayersMoves)
                {
                    line = score.Key + ":" + score.Value;
                    writer.WriteLine(line);
                }
            }
        }

        public bool IsTopResult(int moves)
        {
            var betterUsers = this.PlayersMoves.Where(p => p.Value <= moves).Count();

            return betterUsers < 5;
        }

        private void LoadDataFromFile()
        {
            try
            {
                if (File.Exists("topPlayers.txt"))
                {
                    using (StreamReader reader = new StreamReader(Config.topPlayerFile))
                    {
                        string line;
                        string[] data;
                        int moves;
                        while ((line = reader.ReadLine()) != null)
                        {
                            data = line.Split(':');
                            moves = int.Parse(data[1]);
                            this.PlayersMoves.Add(data[0], moves);
                        }
                    }
                }
            }
            catch (IOException e)
            {
                throw new ApplicationException(e.Message);
            }
        }

        

   
    }
}
