using StarLess;

namespace MiniMAL.Console.Commands
{
    internal class AddAnimeCommand : MiniMALCommand
    {
        public AddAnimeCommand(MiniMALClient client)
            : base(client, "add-anime", "Add an anime in the user's animelist")
        {
            RequiredArguments.Add(new Argument<int>("id", "anime id in MyAnimeList database"));
            RequiredArguments.Add(new Argument<WatchingStatus>("status", "status in your list"));
        }

        protected override void Action(ArgumentsValues args, OptionsValues options)
        {
            ListRequestResult result = Client.AddAnime(args.Value<int>("id"),
                AnimeRequestData.DefaultAddRequest(args.Value<WatchingStatus>("status")));

            switch (result)
            {
                case ListRequestResult.Created:
                    System.Console.WriteLine("Created");
                    break;
                case ListRequestResult.AlreadyInTheList:
                    System.Console.WriteLine("Already in your list.");
                    break;
            }
        }
    }
}