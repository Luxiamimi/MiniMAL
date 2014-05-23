using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Diese.ConsoleInterface;
using Diese.ConsoleInterface.Exceptions;

namespace Diese.ConsoleInterface
{
    // TODO : Optional arguments
    // TODO : Flags
    public abstract class Command
    {
        public string Keyword { get; set; }
        public List<Argument> Arguments { get; set; }
        public List<Argument> OptionalArguments { get; set; }
        public string Description { get; set; }

        protected Command(string keyword, string description = "*no description*")
        {
            Keyword = keyword;
            Arguments = new List<Argument>();
            OptionalArguments = new List<Argument>();
            Description = description;
        }

        public void Run(string[] args)
        {
            int argsCount = 0;
            for (int i = 0; i < args.Length; i++)
            {
                // Flags
                argsCount++;
            }

            if (argsCount != Arguments.Count)
                throw new NumberOfArgumentsException(argsCount, Arguments.Count);

            int j = 0;
            for (int i = 0; i < args.Length; i++)
            {
                // Flags

                if (!Arguments[j].isValid(args[i]))
                    throw new ArgumentNotValidException(Arguments[j], j);

                // Optional Arguments

                j++;
            }

            Action(args);
        }

        protected abstract void Action(string[] args);
    }
}
