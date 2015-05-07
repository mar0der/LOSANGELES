namespace BalloonsPops
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using BalloonsPops.Interfaces;

    public class DataRepository : IDataRepository
    {
        public bool Save(Dictionary<string, int> data, string dataName)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(dataName))
                {
                    string line;
                    foreach (KeyValuePair<string, int> score in data)
                    {
                        line = score.Key + ":" + score.Value;
                        writer.WriteLine(line);
                    }
                }
            }
            catch (Exception)
            {
                throw new ApplicationException("Cannot save the data.");
            }

            return true;
        }

        public Dictionary<string, int> Load(string dataName)
        {
            Dictionary<string, int> fileData = new Dictionary<string, int>();
            try
            {
                if (File.Exists(dataName))
                {
                    using (StreamReader reader = new StreamReader(dataName))
                    {
                        string line;
                        string[] data;
                        int moves;
                        while ((line = reader.ReadLine()) != null)
                        {
                            data = line.Split(':');
                            moves = int.Parse(data[1]);
                            fileData.Add(data[0], moves);
                        }
                    }
                }
            }
            catch (IOException e)
            {
                throw new ApplicationException(e.Message);
            }

            return fileData;
        }
    }
}
