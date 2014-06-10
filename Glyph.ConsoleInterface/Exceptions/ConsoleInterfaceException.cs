using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diese.ConsoleInterface.Exceptions
{
    public abstract class ConsoleInterfaceException : System.Exception
    {
        protected ConsoleInterfaceException(string message) : base(message) { }
    }
}
