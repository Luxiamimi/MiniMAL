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
            MiniMALClient.ListRequestResult result = Client.DeleteManga(args.Value<int>("id"));

            switch (result)
            {
                case MiniMALClient.ListRequestResult.Deleted:
                    System.Console.WriteLine("Deleted");
                    break;
            }
        }
    }
}