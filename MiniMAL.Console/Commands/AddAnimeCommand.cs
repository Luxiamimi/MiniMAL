using StarLess;

namespace MiniMAL.Console.Commands
{
    internal class AddAnimeCommand : MiniMALCommand
    {
        public AddAnimeCommand(MiniMALClient client)
            : base(client, "add-anime", "Add an anime in the user's animelist")
        {
            RequiredArguments.Add(new Argument("id", typeof(int), "anime id in MyAnimeList database"));
            RequiredArguments.Add(new Argument("status", typeof(WatchingStatus),
                "status in your list"));
        }

        protected override void Action(ArgumentsValues args, OptionsValues options)
        {
            ListRequestResult result = Client.AddAnime(args.Value<int>("id"),
                AnimeRequestData.DefaultAddRequest(args.Value<WatchingStatus>("status")));
            
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