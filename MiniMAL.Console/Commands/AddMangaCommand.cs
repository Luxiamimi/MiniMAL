using StarLess;

namespace MiniMAL.Console.Commands
{
    internal class AddMangaCommand : MiniMALCommand
    {
        public AddMangaCommand(MiniMALClient client)
            : base(client, "add-manga", "Add an manga in the user's mangalist")
        {
            RequiredArguments.Add(new Argument("id", typeof(int), "manga id in MyAnimeList database"));
            RequiredArguments.Add(new Argument("status", typeof(ReadingStatus), "status in your list"));
        }

        protected override void Action(ArgumentsValues args, OptionsValues options)
        {
            ListRequestResult result = Client.AddManga(args.Value<int>("id"),
                MangaRequestData.DefaultAddRequest(args.Value<ReadingStatus>("status")));

            switch (result)
            {
                case ListRequestResult.Created:
                    System.Console.WriteLine("Success");
                    break;
                case ListRequestResult.AlreadyInTheList:
                    System.Console.WriteLine("Already in your list.");
                    break;
            }
        }
    }
}