using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;

namespace BalloonsPops.Data
{
    public sealed class TopPlayers
    {
        private static object syncLock = new object();
        private static volatile TopPlayers instance;
        private TopPlayers()
        {
            this.LoadDataFromFile();
            this.PlayersMoves = new Dictionary<string, int>();
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
            this.PlayersMoves.Add(name, moves);
            this.SaveDataToFile();
        }

        public Dictionary<string, int> GetHighScore()
        {
            var tempDict = new Dictionary<string, int>();
            //TODO: sort the dictionary
            return tempDict;

        } 

        private void SaveDataToFile()
        {
            //TODO: write strigified player score
        }

        private void LoadDataFromFile()
        {
            //TODO: load strigified player score
        }

        

   
    }
}
