using System;
using BalloonsPops.Interfaces;

namespace BalloonsPops.Data
{
    public class GameBoard : IGameBoard
    {

        public GameBoard(IEntity[,] entity)
        {
            this.Entities = entity;
        }

        public IEntity[,] Entities { get; private set; }

        private bool MoveDownElementAt(int row, int col)
        {

            if (this.Entities[row + 1, col].Symbol == "." && this.Entities[row, col].Symbol != ".")
            {
                IEntity tempEntity = new Baloon(this.Entities[row,col].Symbol,this.Entities[row,col].Color);
                IEntity emptyEntity = new Baloon(".", new Color(ConsoleColor.White, -1));
                this.Entities[row + 1, col] = tempEntity;
                this.Entities[row, col] = emptyEntity;
                return true;
            }

            return false;
        }

        public void Drop()
        {
            var hasDroped = true;
            while (hasDroped)
            {
                hasDroped = false;
                for (int row = 0; row < this.Entities.GetLength(0) - 1; row++)
                {
                    for (int col = 0; col < this.Entities.GetLength(1); col++)
                    {
                        if (MoveDownElementAt(row, col))
                        {
                            hasDroped = true;
                        }
                    }
                }
            }
        }
    }
}
