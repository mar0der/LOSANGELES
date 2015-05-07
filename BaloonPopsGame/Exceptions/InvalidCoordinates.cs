namespace BalloonsPops.Exceptions
{
    using System;

    public class InvalidCoordinates : ApplicationException
    {
        public InvalidCoordinates(string msg)
            : base(msg)
        {
        }
    }
}
