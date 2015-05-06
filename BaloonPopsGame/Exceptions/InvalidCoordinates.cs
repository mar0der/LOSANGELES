namespace BalloonsPops.Exceptions
{
    using System;

    class InvalidCoordinates : ApplicationException
    {
        public InvalidCoordinates(string msg)
            : base(msg)
        {
        }
    }
}
