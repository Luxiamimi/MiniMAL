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
            Description = "Display a list of all the commands.";

            Commands = commands;
        }

        protected override void Action(ArgumentsDictionary arguments, OptionsDictionary options)
        {
            foreach (Command c in Commands.Values)
            {
                Console.WriteLine();
                Console.Write(c.Keyword);

                foreach (Argument a in c.RequiredArguments)
                    Console.Write(" " + a.Name);
                Console.WriteLine();

                Console.WriteLine("\tDESCRIPTION : " + c.Description);

                if (c.RequiredArguments.Any())
                    Console.WriteLine("\tARGUMENTS :");

                foreach (Argument a in c.RequiredArguments)
                    Console.WriteLine("\t\t" + a.Name + " : " + a.Description);
            }
        }
    }
}
