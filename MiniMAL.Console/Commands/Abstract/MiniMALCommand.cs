using StarLess;

namespace MiniMAL.Console.Commands.Abstract
{
    internal abstract class MiniMALCommand : Command
    {
        protected readonly MiniMALClient Client;

        protected MiniMALCommand(MiniMALClient client, string keyword, string description)
            : base(keyword, description)
        {
            Client = client;
        }
    }
}