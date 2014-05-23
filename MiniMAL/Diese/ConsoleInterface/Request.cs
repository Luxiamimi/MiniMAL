using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diese.ConsoleInterface
{
    public struct Request
    {
        public string Command { get; set; }
        public string[] Arguments { get; set; }

        public Request(string[] args) : this()
        {
            if (!args.Any())
                throw new ArgumentException("ERROR : Empty request !");

            Command = args[0];

            Arguments = new string[args.Length - 1];
            for (int i = 0; i < Arguments.Length; i++)
                Arguments[i] = args[i+1];
        }
    }
}
