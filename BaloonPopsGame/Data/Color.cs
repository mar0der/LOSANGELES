namespace BalloonsPops.Data
{
    using System;

    public class Color
    {
        public Color(ConsoleColor consoleColor, int colorId)
        {
            this.ConsoleColor = consoleColor;
            this.ColorId = colorId;
        }

        public ConsoleColor ConsoleColor { get; set; }

        public int ColorId { get; set; }
    }
}
