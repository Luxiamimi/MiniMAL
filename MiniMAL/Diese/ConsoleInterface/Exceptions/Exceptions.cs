using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diese.ConsoleInterface.Exceptions
{
    public class NumberOfArgumentsException : System.Exception
    {
        public NumberOfArgumentsException(int given, int need)
            : base((given > need ? "Too many arguments" : "Not enough arguments")
                    + ". (given : " + given + ", need : " + need + ")") { }
    }

    public class ArgumentNotValidException : System.Exception
    {
        public ArgumentNotValidException(Argument a, int idArg)
            : base("Unvalid value for argument n°" + idArg + "."
                    + ((a.getUnvalidMessage() != "") ? " (" + a.getUnvalidMessage() + ")" : "")) { }
    }

    public class UnknownCommandException : System.Exception
    {
        public UnknownCommandException(string commandName)
            : base("Unknown command : \"" + commandName + "\".") { }
    }
}
