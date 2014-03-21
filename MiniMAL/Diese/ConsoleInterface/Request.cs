using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diese.ConsoleInterface
{
    public struct Request
    {
        public string Keyword { get; set; }
        public List<string> Arguments { get; set; }

        public Request(string[] args) : this()
        {
            if (!args.Any())
                throw new ArgumentException("ERROR : args est vide !");

            Arguments = new List<string>();
            for (int i = 1; i < args.Length; i++)
                Arguments.Add(args[i]);
            Keyword = args[0];
        }
    }
}
