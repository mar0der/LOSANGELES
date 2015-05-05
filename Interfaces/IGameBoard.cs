namespace BalloonsPops.Interfaces
{
    public interface IGameBoard
    {
        IEntity[,] Entities { get; }

        void Drop();
    }
}
