

using System.Security.Cryptography.X509Certificates;
using System.Xml;
using BalloonsPops.Core;
using BalloonsPops.Interfaces;
using BalloonsPops.Utilities;

namespace BalloonsPops
{
    using System;
    using System.Text.RegularExpressions;
    using BalloonsPops.Data;

    public class Engine
    {
        private bool isGameRunning = true;
        private ConsoleRenderer consoleRenderer;
        public Engine()
        {
            var gameBoardArray = GameBoardGenerator.GenerateGameBoard(Config.GameBoardHeight, Config.GameBoardWidth, Config.MaxColorCount);
            this.GameBoard = new GameBoard(gameBoardArray);
            this.consoleRenderer = new ConsoleRenderer();
        }

        public GameBoard GameBoard { get; set; }

        public void Run()
        {
            while (isGameRunning)
            {
                this.ExecuteLoop();
            }
        }

        protected virtual void ExecuteLoop()
        {
            //Render All Objects
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
                    //TODO: Show Hightscore
                    break;
                case "restart":
                    //TODO: Restart da game
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

        private void Shoot(int[] coordinates)
        {
            var gameBoard = this.GameBoard.Entities;
            this.PopUp(gameBoard, coordinates);
            this.PopDown(gameBoard, coordinates);
            this.PopLeft(gameBoard, coordinates);
            this.PopRight(gameBoard, coordinates);
            this.Pop(gameBoard[coordinates[1], coordinates[0]]);
            this.GameBoard.Drop();

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
