using StarLess;

namespace MiniMAL.Console.Commands
{
    public abstract class MiniMALUnlimitedCommand : UnlimitedCommand
    {
        protected MiniMALClient _client;

        protected MiniMALUnlimitedCommand(MiniMALClient client, string keyword, string description)
            : base(keyword, description)
        {
            this._client = client;
        }
    }
}