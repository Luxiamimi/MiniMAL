using System;

namespace MiniMAL.Exceptions
{
    public class ConfigFileCorruptException : Exception
    {
        public ConfigFileCorruptException()
            : base("The configuration file is corrupt.")
        {
        }
    }
}