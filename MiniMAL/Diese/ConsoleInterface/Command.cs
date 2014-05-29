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
        public Dictionary<string, Option> Options { get; set; }
        public string Description { get; set; }

        protected Command(string keyword, string description = "*no description*")
        {
            Keyword = keyword;
            RequiredArguments = new List<Argument>();
            OptionalArguments = new List<Argument>();
            Options = new Dictionary<string, Option>();
            Description = description;
        }

        public void Run(string[] args)
        {
            int argsCount = 0;
            for (int i = 0; i < args.Length; i++)
            {
                if (Options.Keys.Contains(args[i]))
                {
                    i += Options[args[i]].Arguments.Count;
                    continue;
                }

                argsCount++;
            }

            if (argsCount < RequiredArguments.Count)
                throw new NumberOfArgumentsException(argsCount, RequiredArguments.Count);

            if (argsCount > RequiredArguments.Count + OptionalArguments.Count)
                throw new NumberOfArgumentsException(argsCount, RequiredArguments.Count + OptionalArguments.Count);

            int j = 0;
            int k = 0;
            for (int i = 0; i < args.Length; i++)
            {
                if (Options.Keys.Contains(args[i]))
                {
                    foreach (Argument a in Options[args[i]].Arguments)
                    {
                        i++;
                        if (!a.isValid(args[i]))
                            throw new ArgumentNotValidException(RequiredArguments[j], j);
                    }
                    continue;
                }

                if (j < RequiredArguments.Count)
                {
                    if (!RequiredArguments[j].isValid(args[i]))
                        throw new ArgumentNotValidException(RequiredArguments[j], j);
                    j++;
                }
                else
                {
                    if (!OptionalArguments[k].isValid(args[i]))
                        throw new ArgumentNotValidException(OptionalArguments[k], j+k);
                    k++;
                }
            }

            Action(args);
        }

        protected abstract void Action(string[] args);
    }
}
