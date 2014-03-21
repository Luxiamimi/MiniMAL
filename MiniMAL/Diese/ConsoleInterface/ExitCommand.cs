using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diese.ConsoleInterface
{
    public class ExitCommand : Command
    {
        public ExitCommand() : base("exit")
        {
        }

        protected override void Action(List<string> args)
        {
        }
    }
}
