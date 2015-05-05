using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BalloonsPops
{
    class Command
    {

        string command;

        public string Value
        {
            get
            {
                return this.command;
            }
            set
            {
                this.command = value;
            }
        }

        public static bool TryParse(string input, ref Command result)
        {
            if (input == "top")
            {
                result.Value = input;
                return true;
            }

            if (input == "restart")
            {
                result.Value = input;
                return true;
            }

            if (input == "exit")
            {
                result.Value = input;
                return true;
            }

            return false;
        }
    }
}
