using Diese.ConsoleInterface;

namespace MiniMAL.ConsoleInterface.Commands
{
    public abstract class MiniMALCommand : Command
    {
        protected MiniMALClient client;

        protected MiniMALCommand(MiniMALClient client, string keyword, string description)
            : base(keyword, description)
        {
            this.client = client;
        }
    }
}