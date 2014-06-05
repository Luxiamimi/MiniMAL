using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Diese.ConsoleInterface;
using Diese.ConsoleInterface.Exceptions;

namespace Diese.ConsoleInterface
{
    // TODO : Check if an argument appears twice
    // TODO : Check if an argument is a mistake
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

            ArgumentsDictionary arguments = new ArgumentsDictionary();
            OptionsDictionary options = new OptionsDictionary();

            int j = 0;
            int k = 0;
            for (int i = 0; i < args.Length; i++)
            {
                if (Options.Keys.Contains(args[i]))
                {
                    int indexOption = i;

                    ArgumentsDictionary optionArgs = new ArgumentsDictionary();
                    for (int x = 0; x < Options[args[indexOption]].Arguments.Count; x++)
                    {
                        i++;
                        Argument a = Options[args[indexOption]].Arguments[x];
                        if (!a.isValid(args[i]))
                            throw new ArgumentNotValidException(a, j);

                        optionArgs.Add(a.Name, args[i]);
                    }
                    options.Add(args[indexOption], optionArgs);
                    continue;
                }

                if (j < RequiredArguments.Count)
                {
                    if (!RequiredArguments[j].isValid(args[i]))
                        throw new ArgumentNotValidException(RequiredArguments[j], j);

                    arguments.Add(RequiredArguments[j].Name, args[i]);
                    j++;
                }
                else
                {
                    if (!OptionalArguments[k].isValid(args[i]))
                        throw new ArgumentNotValidException(OptionalArguments[k], j + k);

                    arguments.Add(OptionalArguments[k].Name, args[i]);
                    k++;
                }
            }

            Action(arguments, options);
        }

        protected abstract void Action(ArgumentsDictionary arguments, OptionsDictionary options);

        protected class ArgumentsDictionary : Dictionary<string, string>
        {
        }

        protected class OptionsDictionary : Dictionary<string, ArgumentsDictionary>
        {
        }
    }
}
