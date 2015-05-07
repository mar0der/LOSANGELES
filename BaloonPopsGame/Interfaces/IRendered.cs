using System.Security.Cryptography.X509Certificates;

namespace BalloonsPops.Interfaces
{
    interface IRenderer
    {
        void RenderGameBoard(IGameBoard gameBoard);
    }
}
