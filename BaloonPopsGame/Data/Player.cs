namespace BalloonsPops.Data
{
    using BalloonsPops.Exceptions;
    using System;
    using System.Text.RegularExpressions;

    public class Player
    {
        private string name;
            
        public Player()
        {
        }

        public string Name
        {
            get 
            {
                return this.name; 
            }
            set
            {
                string clearValue = value.Trim();
                clearValue = Regex.Replace(clearValue, " {2,}", " ");
                if (clearValue.Length < 2 || Regex.IsMatch(clearValue, "[^a-zA-Z0-9 _]+"))
                {
                    throw new InvalidCommand("The name must be at least 2 characters, only letters, digits and space.");
                }

                this.name = clearValue;
            }
        }

        public int CurrentMoves { get; set; }
    }
}
