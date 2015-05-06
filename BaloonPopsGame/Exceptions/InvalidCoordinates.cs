using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BalloonsPops.Exceptions
{
    class InvalidCoordinates : ApplicationException
    {
        public InvalidCoordinates(string msg)
            : base(msg)
        {
        }
    }
}
