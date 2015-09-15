using System;

namespace MiniMAL.Exceptions
{
    public abstract class MiniMALException : Exception
    {
        internal protected MiniMALException(string message)
            : base(message)
        {
        }
    }
}