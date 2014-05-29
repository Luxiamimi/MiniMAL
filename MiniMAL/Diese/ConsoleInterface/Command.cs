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
        public List<Argument> RequiredArguments { get; set; }
        public List<Argument> OptionalArguments { get; set; }
        public string Description { get; set; }

        protected Command(string keyword, string description = "*no description*")
        {
            Keyword = keyword;
            RequiredArguments = new List<Argument>();
            OptionalArguments = new List<Argument>();
            Description = description;
        }

        public void Run(string[] args)
        {
            int argsCount = 0;
            for (int i = 0; i < args.Length; i++)
            {
                // Flags
                // Optional Arguments
                argsCount++;
            }

            if (argsCount != RequiredArguments.Count)
                throw new NumberOfArgumentsException(argsCount, RequiredArguments.Count);

            int j = 0;
            for (int i = 0; i < args.Length; i++)
            {
                // Flags

                if (!RequiredArguments[j].isValid(args[i]))
                    throw new ArgumentNotValidException(RequiredArguments[j], j);

                // Optional Arguments

                j++;
            }

            Action(args);
        }

        protected abstract void Action(string[] args);
    }
}
