using System;

namespace MiniMAL.Exceptions
{
    public class ConfigFileNotFoundException : Exception
    {
        public ConfigFileNotFoundException()
            : base("Configuration file not found ! This must be your first use.")
        {
        }
    }
}