using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diese.ConsoleInterface.Exceptions
{
    public class UnknownCommandException : ConsoleInterfaceException
    {
        public UnknownCommandException(string commandName)
            : base("Unknown command : \"" + commandName + "\".") { }
    }
}
