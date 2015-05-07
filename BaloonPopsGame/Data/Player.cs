namespace BalloonsPops.Data
{
    using System.Text.RegularExpressions;
    using BalloonsPops.Exceptions;

    public class Player
    {
        private string name;
 
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
