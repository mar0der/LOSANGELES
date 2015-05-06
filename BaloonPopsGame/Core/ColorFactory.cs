using System;
using BalloonsPops.Data;

namespace BalloonsPops.Core
{
    public static class ColorFactory
    {
        private static Random random = new Random();
        private const int MaxColorNumber = 4;

        public static Color getRandomColor()
        {
            var randomColorId = random.Next(MaxColorNumber);
            switch (randomColorId)
            {
                case 0:
                    return new Color(ConsoleColor.Red, randomColorId);
                case 1:
                    return new Color(ConsoleColor.Green, randomColorId);
                case 2:
                    return new Color(ConsoleColor.Blue, randomColorId);
                case 3:
                    return new Color(ConsoleColor.Yellow, randomColorId);
                default:
                    throw new ArgumentException("Invalid color id!");
            }
        }
    }
}
