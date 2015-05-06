

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

        private string ReadCommand()
        {
            Console.Write("Enter a row and column (space separated): ");
            string command = Console.ReadLine();
            return command.Trim();
        }

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

        private void Pop(IEntity entity)
        {
            entity.Color.ConsoleColor = ConsoleColor.White;
            entity.Symbol = ".";
        }


    }
}
