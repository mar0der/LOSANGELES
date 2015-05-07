using System;
using BalloonsPops.Data;
using BalloonsPops.Interfaces;

namespace BalloonsPops.Core
{
    public class EntityPopper
    {
        private IEntity[,] gameBoard;

        public EntityPopper(IEntity[,] gameBoard)
        {
            this.gameBoard = gameBoard;
        }

        /// <summary>
        /// Checks the upper baloons if it is the same like the shooted one and take them down
        /// </summary>
        /// <param name="gameBoard">game field</param>
        /// <param name="coordinates">entared coordinates</param>
        public void PopUp(int[] coordinates)
        {
            int row = coordinates[0];
            int col = coordinates[1];
            int rowCounter = row;
            while (rowCounter > 0)
            {
                rowCounter--;
                if (this.gameBoard[rowCounter, col].Equals(this.gameBoard[row, col]))
                {
                    this.Pop(new int[] { rowCounter, col });
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
        public void PopDown(int[] coordinates)
        {
            int row = coordinates[0];
            int col = coordinates[1];
            int rowCounter = row;
            while (rowCounter < gameBoard.GetLength(0) - 1)
            {
                rowCounter++;
                if (gameBoard[rowCounter, col].Equals(gameBoard[row, col]))
                {
                    this.Pop(new int[] { rowCounter, col });
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
        public void PopLeft(int[] coordinates)
        {
            int row = coordinates[0];
            int col = coordinates[1];
            int colCounter = col;
            while (colCounter > 0)
            {
                colCounter--;
                if (gameBoard[row, colCounter].Equals(gameBoard[row, col]))
                {
                    this.Pop(new int[] { row, colCounter });
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
        public void PopRight(int[] coordinates)
        {
            int row = coordinates[0];
            int col = coordinates[1];
            int colCounter = col;
            while (colCounter < gameBoard.GetLength(1) - 1)
            {
                colCounter++;
                if (gameBoard[row, colCounter].Equals(gameBoard[row, col]))
                {
                    this.Pop(new int[] { row, colCounter });
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
        public void Pop(int[] coordinates)
        {
            int row = coordinates[0];
            int col = coordinates[1];
            var entity = this.gameBoard[row, col];
            entity.Color.ConsoleColor = ConsoleColor.White;
            entity.Symbol = ".";
        }
    }
}
