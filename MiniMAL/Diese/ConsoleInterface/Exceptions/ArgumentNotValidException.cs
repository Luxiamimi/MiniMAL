﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diese.ConsoleInterface.Exceptions
{
    public class ArgumentNotValidException : ConsoleInterfaceException
    {
        public ArgumentNotValidException(Argument a, int idArg)
            : base("Unvalid value for argument n°" + idArg + "."
                    + ((a.getUnvalidMessage() != "") ? " (" + a.getUnvalidMessage() + ")" : "")) { }
    }
}