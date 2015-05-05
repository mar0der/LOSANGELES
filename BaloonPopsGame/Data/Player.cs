namespace BalloonsPops.Data
{
    using System;

    public class Player
    {
        private string name;
        public Player(string name)
        {
            this.Name = name;
        }

        public string Name
        {
            get { return this.name; }
            set
            {
                if (value.Length < 2)
                {
                    throw new ArgumentException("The name must be at least 2 characters");
                }
                this.name = value;
            }
        }

        public int CurrentMoves { get; set; }
    }
}
