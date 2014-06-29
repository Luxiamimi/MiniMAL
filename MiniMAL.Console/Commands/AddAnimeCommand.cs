using System;
using StarLess;

namespace MiniMAL.Console.Commands
{
    public class AddAnimeCommand : MiniMALCommand
    {
        public AddAnimeCommand(MiniMALClient client)
            : base(client, "add-anime", "Add an anime in the user's animelist")
        {
            RequiredArguments.Add(new Argument("id", "anime id in MyAnimeList database", Validator.TryParse<int>("id")));
            RequiredArguments.Add(new Argument("status", "status in your list", Validator.TryParseEnum<WatchingStatus>("status")));
        }

        protected override void Action(ArgumentsValues arguments, OptionsValues options)
        {
            _client.AddAnime(int.Parse(arguments["id"]),
                AnimeRequestData.DefaultAddRequest((WatchingStatus)Enum.Parse(typeof(WatchingStatus), arguments["status"])));
        }
    }
}