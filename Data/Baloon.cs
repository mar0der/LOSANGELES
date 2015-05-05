using System;
using System.ComponentModel;

namespace BalloonsPops.Data
{
    using BalloonsPops.Interfaces;

    public class Baloon : IEntity
    {
        public Baloon(string symbol, Color color)
        {
            this.Color = color;
            this.Symbol = symbol;
        }

        public Color Color { get; set; }
        public string Symbol { get; set; }

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
