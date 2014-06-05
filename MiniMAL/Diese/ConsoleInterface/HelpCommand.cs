using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diese.ConsoleInterface
{
    public class HelpCommand : Command
    {
        public Dictionary<string, Command> Commands { get; set; }

        public HelpCommand(Dictionary<string, Command> commands) : base("help")
        {
            Commands = commands;
            Description = "Display a list of all the commands.";

            OptionalArguments.Add(new Argument("command", "name of a command", new Validator(x => Commands.ContainsKey(x), "Unknown command")));
        }

        protected override void Action(ArgumentsDictionary arguments, OptionsDictionary options)
        {
            if (arguments.ContainsKey("command"))
                DisplayCommandDescription(Commands[arguments["command"]]);
            else
                foreach (Command c in Commands.Values)
                    DisplayCommandDescription(c, true);
        }

        protected virtual void DisplayCommandDescription(Command c, bool summary = false)
        {
            Console.WriteLine();
            Console.Write(c.Keyword);

            foreach (Argument a in c.RequiredArguments)
                Console.Write(" " + a.Name);

            if (c.OptionalArguments.Any())
            {
                Console.Write(" (");
                foreach (Argument a in c.OptionalArguments)
                    Console.Write(" " + a.Name);
                Console.Write(" )");
            }

            if (c.Options.Any())
            {
                Console.Write(" [");
                foreach (Option o in c.Options.Values)
                    Console.Write(" " + o.Keyword);
                Console.Write(" ]");
            }
            Console.WriteLine();

            Console.WriteLine("\tDESCRIPTION : " + c.Description);

            if (summary)
                return;

            if (c.RequiredArguments.Any())
                Console.WriteLine("\tARGUMENTS :");

            foreach (Argument a in c.RequiredArguments)
                Console.WriteLine("\t\t" + a.Name + " : " + a.Description);

            if (c.Options.Any())
                Console.WriteLine("\tOPTIONS :");

            foreach (Option o in c.Options.Values)
                Console.WriteLine("\t\t" + o.Keyword + " : " + o.Description);
        }
    }
}
