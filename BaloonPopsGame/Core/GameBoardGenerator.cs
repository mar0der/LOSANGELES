﻿namespace BalloonsPops.Core
{
    using System;
    using Data;

    public static class GameBoardGenerator
    {
        public static Baloon[,] GenerateGameBoard(int boardHeight, int boardWidth, int maxCoulorCount)
        {
            if (boardHeight <= 0 || boardWidth <= 0 || maxCoulorCount <= 0)
            {
                throw new ApplicationException("The board size and color count must be positive number");
            }

            var gameBoard = new Baloon[boardHeight, boardWidth];
            Baloon newBaloon;
            Color color;

            for (int row = 0; row < boardHeight; row++)
            {
                for (int col = 0; col < boardWidth; col++)
                {
                    color = ColorFactory.GetRandomColor(maxCoulorCount);
                    newBaloon = new Baloon(color.ColorId.ToString(), color);
                    gameBoard[row, col] = newBaloon;
                }
            }

            return gameBoard;
        }
    }
}
