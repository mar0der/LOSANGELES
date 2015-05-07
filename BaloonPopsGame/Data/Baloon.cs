namespace BalloonsPops.Data
{
    using System;
    using Interfaces;

    public class Baloon : IEntity
    {
        private Color color;
        private string symbol;

        public Baloon(string symbol, Color color)
        {
            this.Color = color;
            this.Symbol = symbol;
        }

        public Color Color
        {
            get
            {
                return this.color;
            }

            set
            {
                if (value == null)
                {
                    throw new ApplicationException("The color cannot be empty.");
                }

                this.color = value;
            }
        }

        public string Symbol
        {
            get
            {
                return this.symbol;
            }

            set
            {
                if (value.Length == 0)
                {
                    throw new ApplicationException("The symbol cannot be empty.");
                }

                this.symbol = value;
            }
        }

        // override object.Equals
        public override bool Equals(object obj)
        {
            if (!(obj is Baloon))
            {
                return false;
            }

            return this.Symbol.CompareTo(((Baloon)obj).Symbol) == 0 ? true : false;
        }
    }
}
