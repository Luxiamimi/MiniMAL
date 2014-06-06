using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Diese.ConsoleInterface;

namespace MiniMAL.ConsoleInterface.Commands
{
    public abstract class MiniMALCommand : Command
    {
        protected MiniMALClient client;

        protected MiniMALCommand(MiniMALClient client, string keyword) : base(keyword)
        {
            this.client = client;
        }
    }
}
