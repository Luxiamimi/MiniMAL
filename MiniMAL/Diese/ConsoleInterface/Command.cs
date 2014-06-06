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
        public string Description { get; set; }

        public List<Argument> RequiredArguments { get; set; }
        public List<Argument> OptionalArguments { get; set; }
        public Dictionary<string, Option> Options { get; set; }
        public bool UnlimitedArguments { get; set; }

        protected Command(string keyword)
        {
            Keyword = keyword;
            Description = "*no description*";
            RequiredArguments = new List<Argument>();
            OptionalArguments = new List<Argument>();
            Options = new Dictionary<string, Option>();
            UnlimitedArguments = false;
        }

        public void Run(string[] args)
        {
            if (!UnlimitedArguments)
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
            }

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

                if (UnlimitedArguments)
                {
                    arguments.Add(j.ToString(), args[i]);
                    j++;
                }
                else
                {
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
            }

            Action(arguments, options);
        }

        protected abstract void Action(ArgumentsDictionary arguments, OptionsDictionary options);

        protected class ArgumentsDictionary : Dictionary<string, string>
        {
            public string this[int index]
            {
                get
                {
                    if (index < 0 || index >= Count)
                        throw new IndexOutOfRangeException();
                    else
                        return this.ElementAt(index).Value;
                }
            }
        }

        protected class OptionsDictionary : Dictionary<string, ArgumentsDictionary>
        {
        }
    }
}
