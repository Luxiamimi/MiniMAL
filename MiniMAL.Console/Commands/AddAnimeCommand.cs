using StarLess;

namespace MiniMAL.Console.Commands
{
    public class AddAnimeCommand : MiniMALCommand
    {
        public AddAnimeCommand(MiniMALClient client)
            : base(client, "add-anime", "Add an anime in the user's animelist")
        {
            RequiredArguments.Add(new Argument("id", typeof(int), "anime id in MyAnimeList database"));
            RequiredArguments.Add(new Argument("status", typeof(WatchingStatus), "status in your list"));
        }

        protected override void Action(ArgumentsValues arguments, OptionsValues options)
        {
            Client.AddAnime((int)arguments["id"], AnimeRequestData.DefaultAddRequest((WatchingStatus)arguments["status"]));
        }
    }
}