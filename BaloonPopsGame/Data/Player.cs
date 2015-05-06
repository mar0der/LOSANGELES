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

                if (!Regex.IsMatch(value, @"^\w{2,}$"))
                {
                    throw new InvalidCommand("The name must be at least 2 characters, only letters and digits.");
                }

                this.name = value;
            }
        }

        public int CurrentMoves { get; set; }
    }
}
