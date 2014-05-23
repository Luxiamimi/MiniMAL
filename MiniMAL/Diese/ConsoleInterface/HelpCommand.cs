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

        protected override void Action(string[] args)
        {
            foreach (Command c in Commands.Values)
            {
                Console.WriteLine("\t" + c.Keyword + " : " + c.Description);
            }
        }
    }
}
