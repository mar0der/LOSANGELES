using System;
using System.Runtime.Remoting.Channels;

namespace BalloonsPops.Utilities
{
    using BalloonsPops.Interfaces;

    public class ConsoleRenderer : IRenderer
    {
        /// <summary>
        /// Class for rendering the gamebord to the console screen
        /// </summary>
        /// <param name="gameBoard">An Object that implements Igameboard</param>
        public void Render(IGameBoard gameBoard)
        {
            Console.ForegroundColor = ConsoleColor.White;
            for (int i = 0; i < gameBoard.Entities.GetLength(1) + 1; i++)
            {
                if (i == 0)
                {
                    Console.Write("    ");
                }
                else
                {
                    Console.Write(i - 1 + " ");
                }
            }
            Console.WriteLine();
            for (int i = 0; i < gameBoard.Entities.GetLength(1) + 1; i++)
            {
                if (i == 0)
                {
                    Console.Write("    ");
                }
                else
                {
                    Console.Write("- ");
                }
            }
            Console.WriteLine();

            var rowCounter = 0;

            for (var row = 0; row < gameBoard.Entities.GetLength(0); row++)
            {
                for (var col = 0; col < gameBoard.Entities.GetLength(1) + 1; col++)
                {
                    if (col == 0)
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write(rowCounter + " | ");
                    }
                    else
                    {
                        Console.ForegroundColor = gameBoard.Entities[row, col - 1].Color.ConsoleColor;
                        Console.Write(gameBoard.Entities[row, col - 1].Symbol + " ");

                    }
                }
                Console.WriteLine();
                Console.WriteLine();
                rowCounter++;
            }

            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
