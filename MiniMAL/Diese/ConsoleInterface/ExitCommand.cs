using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diese.ConsoleInterface
{
    public class ExitCommand : Command
    {
        public ExitCommand(string applicationName) : base("exit")
        {
            Description = "Exit " + applicationName + ".";
        }

        protected override void Action(string[] args)
        {
        }
    }
}
