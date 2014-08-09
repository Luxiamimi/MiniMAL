using MiniMAL.Console.Commands.Abstract;
using StarLess;

namespace MiniMAL.Console.Commands
{
    internal class DeleteMangaCommand : MiniMALCommand
    {
        public DeleteMangaCommand(MiniMALClient client)
            : base(client, "delete-manga", "Delete an manga in the user's mangalist")
        {
            RequiredArguments.Add(new Argument<int>("id", "manga id in MyAnimeList database"));
        }

        protected override void Action(ArgumentsValues args, OptionsValues options)
        {
            MiniMALClient.DeleteRequestResult result = Client.DeleteManga(args.Value<int>("id"));

            switch (result)
            {
                case MiniMALClient.DeleteRequestResult.Deleted:
                    System.Console.WriteLine("Deleted");
                    break;
            }
        }
    }
}