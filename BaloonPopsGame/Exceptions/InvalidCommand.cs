namespace BalloonsPops.Exceptions
{
    using System;

    public class InvalidCommand : ApplicationException
    {
        public InvalidCommand(string msg)
            : base(msg)
        {
        }
    }
}
