using System;

namespace MiniMAL.Exceptions
{
    public abstract class MiniMALException : Exception
    {
        protected internal MiniMALException(string message)
            : base(message) {}
    }
}