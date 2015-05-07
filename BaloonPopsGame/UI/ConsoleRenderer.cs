using System.Collections.Generic;

namespace BalloonsPops.Utilities
{
    using System;
    using System.Text;
    using BalloonsPops.Interfaces;

    public class ConsoleRenderer : IRenderer
    {
        /// <summary>
        /// Class for rendering the gamebord to the console screen
        /// </summary>
        /// <param name="gameBoard">An Object that implements Igameboard</param>
        public void RenderGameBoard(IGameBoard gameBoard)
        {
            Console.ForegroundColor = ConsoleColor.White;
            this.BuildHeader(gameBoard);

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

        private void BuildHeader(IGameBoard gameBoard)
        {
            var output = new StringBuilder();
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
        }

        public void PrintTopPlayers(Dictionary<string, int> playerMoves )
        {
            Console.WriteLine("Scoreboard:");
            int playerPosition = 0; ;
            if (playerMoves.Count > 0)
            {
                foreach (KeyValuePair<string, int> player in playerMoves)
                {
                    playerPosition++;
                    Console.WriteLine("{0}. {1} --> {2}", playerPosition, player.Key, player.Value);
                }
            }
            else
            {
                Console.WriteLine("No saved results.");
            }
        }

        public void PrintCurrentScore(int currentMoves)
        {
            Console.WriteLine("Your moves: {0}", currentMoves);
        }

        public void PrintStaticText(string text)
        {
            Console.WriteLine(text);
        }
    }
}
