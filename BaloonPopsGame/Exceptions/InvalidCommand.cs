using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BalloonsPops.Exceptions
{
    public class InvalidCommand : ApplicationException
    {
        public InvalidCommand(string msg)
            : base(msg)
        {

        }
    }
}
