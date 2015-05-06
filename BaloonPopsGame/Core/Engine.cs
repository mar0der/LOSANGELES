

using System.Security.Cryptography.X509Certificates;
using System.Xml;
using System.Linq;
using BalloonsPops.Core;
using BalloonsPops.Interfaces;
using BalloonsPops.Utilities;

namespace BalloonsPops
{
    using System;
    using System.Text.RegularExpressions;
    using BalloonsPops.Data;
    using System.Collections.Generic;

    public class Engine
    {

        private bool isGameRunning = true;
        private ConsoleRenderer consoleRenderer;
        private Player player;
        private TopPlayers topPlayers;
        

        public Engine()
        {
        }

        public GameBoard GameBoard { get; set; }
        /// <summary>
        /// The Game starting loop
        /// </summary>
        public void Run()
        {
            topPlayers = TopPlayers.Instance;

            while (true)
            {
                try
                {
                    Console.Write("Please enter your name:");
                    var name = Console.ReadLine();
                    player = new Player(name);
                    break;
                }
                catch (ArgumentException e)
                {
                    Console.WriteLine(e.Message);
                    //TODO: catch all errors
                }
            }

            this.StartNewGame();
            this.consoleRenderer = new ConsoleRenderer();
            
            while (isGameRunning)
            {
                this.ExecuteLoop();
            }
        }
        /// <summary>
        /// Method that controll all operations in the game loop
        /// </summary>
        protected virtual void ExecuteLoop()
        {
            //Render All Objects
            Console.Clear();
            this.PrintStaticText();
            this.PrintCurrentScore();
            this.consoleRenderer.Render(this.GameBoard);
            //Read Input
            this.ExecuteCommand(this.ReadCommand());
            //some logc
        }

        /// <summary>
        /// Reads current command from the console
        /// </summary>
        /// <returns>Returns clear command</returns>
        private string ReadCommand()
        {
            Console.Write("Enter a row and column (space separated): ");
            string command = Console.ReadLine();
            return command.Trim();
        }
        /// <summary>
        /// Parse the command and calls the appropriate method
        /// </summary>
        /// <param name="command">Entered command</param>
        private void ExecuteCommand(string command)
        {
            switch (command)
            {
                case "exit":
                    this.isGameRunning = false;
                    break;
                case "top":
                    this.printTopPlayers();
                    break;
                case "restart":
                    this.StartNewGame();
                    break;
                default:
                    int[] cordinates = CordinateParser(command);
                    this.Shoot(cordinates);
                    break;
            }
        }

        /// <summary>
        /// Checks and parse the coordinates
        /// </summary>
        /// <param name="stringCoordinates">Entered cooradinates from user</param>
        /// <returns>A valid coordinates</returns>
        private int[] CordinateParser(string stringCoordinates)
        {
            var coordinatesStringArray = Regex.Split(stringCoordinates, @"\s+");

            int x;
            int y;
            if (int.TryParse(coordinatesStringArray[0], out y) && int.TryParse(coordinatesStringArray[1], out x))
            {
                //TODO: validate coordinates 
                return new int[]{x,y};
            }

            throw new ApplicationException("Invalid command.");
        }

        private void PrintStaticText()
        {
            Console.WriteLine("Welcome to \"Balloons Pops\" game. Please try to pop the balloons. Use 'top' to view the top scoreboard,'restart' to start a new game and 'exit' to quit the game.");
        }

        private void printTopPlayers(){
            foreach (KeyValuePair<string, int> player in topPlayers.PlayersMoves)
            {
                Console.WriteLine(player.Key + ":" + player.Value);
            }
        }


        private void PrintCurrentScore()
        {
            Console.WriteLine("Your moves: {0}", player.CurrentMoves);
        }

        /// <summary>
        /// Checks if the game is over
        /// </summary>
        /// <returns>return the current stage of the game</returns>
        private bool IsGameOver()
        {
            var isGameOver = true;
            for (int row = 0; row < this.GameBoard.Entities.GetLength(0); row++)
            {
                for (int col = 0; col < this.GameBoard.Entities.GetLength(1); col++)
                {
                    if (this.GameBoard.Entities[row, col].Symbol != ".")
                    {
                        isGameOver = false;
                    }
                }
            }
            return isGameOver;
        }


        private void StartNewGame()
        {
            this.player.CurrentMoves = 0;
            var gameBoardArray = GameBoardGenerator.GenerateGameBoard(Config.GameBoardHeight, Config.GameBoardWidth, Config.MaxColorCount);
            this.GameBoard = new GameBoard(gameBoardArray);
        }
        /// <summary>
        /// Shoot the baloon from the current coordinates and take them down :):D(:
        /// </summary>
        /// <param name="coordinates">entard coordinates</param>
        private void Shoot(int[] coordinates)
        {
            var gameBoard = this.GameBoard.Entities;
            this.PopUp(gameBoard, coordinates);
            this.PopDown(gameBoard, coordinates);
            this.PopLeft(gameBoard, coordinates);
            this.PopRight(gameBoard, coordinates);
            this.Pop(gameBoard[coordinates[1], coordinates[0]]);
            this.GameBoard.Drop();
            this.player.CurrentMoves++;
            if (IsGameOver())
            {
                Console.WriteLine("Game Over");
                this.topPlayers.AddScore(this.player.Name, this.player.CurrentMoves);
                this.StartNewGame();
            }

        }
        /// <summary>
        /// Checks the upper baloons if it is the same like the shooted one and take them down
        /// </summary>
        /// <param name="gameBoard">game field</param>
        /// <param name="coordinates">entared coordinates</param>
        private void PopUp(IEntity[,] gameBoard, int[] coordinates )
        {
            int col = coordinates[0];
            int row = coordinates[1];
            int rowCounter = row;
            while (rowCounter > 0)
            {
                rowCounter--;
                if (gameBoard[rowCounter, col].Equals(gameBoard[row, col]))
                {
                    this.Pop(gameBoard[rowCounter, col]);
                }
                else
                {
                    break;
                }
            }
        }
        /// <summary>
        /// Checks if the down baloons is the same as the shooted one and take them down
        /// </summary>
        /// <param name="gameBoard">game field</param>
        /// <param name="coordinates">entared coordinates</param>
        private void PopDown(IEntity[,]  gameBoard, int[] coordinates)
        {
            int col = coordinates[0];
            int row = coordinates[1];
            int rowCounter = row;
            while (rowCounter < gameBoard.GetLength(0) - 1)
            {
                rowCounter++;
                if (gameBoard[rowCounter, col].Equals(gameBoard[row, col]))
                {
                    this.Pop(gameBoard[rowCounter, col]);
                }
                else
                {
                    break;
                }
            }
        }
        /// <summary>
        /// Checks the left side baloons if it is the same like the shooted one and take them down
        /// </summary>
        /// <param name="gameBoard">game field</param>
        /// <param name="coordinates">entared coordinates</param>
        private void PopLeft(IEntity[,] gameBoard, int[] coordinates)
        {
            int col = coordinates[0];
            int row = coordinates[1];
            int colCounter = col;
            while (colCounter > 0)
            {
                colCounter--;
                if (gameBoard[row, colCounter].Equals(gameBoard[row, col]))
                {
                    this.Pop(gameBoard[row, colCounter]);
                }
                else
                {
                    break;
                }
            }
        }
        /// <summary>
        /// Checks the right side baloons if it is the same like the shooted one and take them down
        /// </summary>
        /// <param name="gameBoard">game field</param>
        /// <param name="coordinates">entared coordinates</param>
        private void PopRight(IEntity[,] gameBoard, int[] coordinates)
        {
            int col = coordinates[0];
            int row = coordinates[1];
            int colCounter = col;
            while (colCounter < gameBoard.GetLength(1) - 1)
            {
                colCounter++;
                if (gameBoard[row, colCounter].Equals(gameBoard[row, col]))
                {
                    this.Pop(gameBoard[row, colCounter]);
                }
                else
                {
                    break;
                }
            }
        }
        /// <summary>
        /// Replace the shooted baloon with the dot
        /// </summary>
        /// <param name="entity">current shooted baloon</param>
        private void Pop(IEntity entity)
        {
            entity.Color.ConsoleColor = ConsoleColor.White;
            entity.Symbol = ".";
        }


    }
}
