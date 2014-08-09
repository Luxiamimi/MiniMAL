using MiniMAL.Console.Commands.Abstract;
using StarLess;

namespace MiniMAL.Console.Commands
{
    internal class DeleteAnimeCommand : MiniMALCommand
    {
        public DeleteAnimeCommand(MiniMALClient client)
            : base(client, "delete-anime", "Delete an anime in the user's animelist")
        {
            RequiredArguments.Add(new Argument<int>("id", "anime id in MyAnimeList database"));
        }

        protected override void Action(ArgumentsValues args, OptionsValues options)
        {
            MiniMALClient.ListRequestResult result = Client.DeleteAnime(args.Value<int>("id"));

            switch (result)
            {
                case MiniMALClient.ListRequestResult.Deleted:
                    System.Console.WriteLine("Deleted");
                    break;
            }
        }
    }
}