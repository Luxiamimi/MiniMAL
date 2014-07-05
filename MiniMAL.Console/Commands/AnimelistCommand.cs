using System.Collections.Generic;
using System.Linq;
using StarLess;

namespace MiniMAL.Console.Commands
{
    public class AnimelistCommand : MiniMALCommand
    {
        public AnimelistCommand(MiniMALClient client)
            : base(client, "animelist", "Display the anime list from a user.")
        {
            OptionalArguments.Add(new Argument("user", typeof(string), "a MyAnimeList's username. (connected user if not stated)"));

            Options.Add(new Option("w", "watching", "Select currently watching entries."));
            Options.Add(new Option("c", "completed", "Select completed entries."));
            Options.Add(new Option("h", "hold", "Select on-hold entries."));
            Options.Add(new Option("d", "dropped", "Select dropped entries."));
            Options.Add(new Option("p", "planned", "Select plan to read entries."));
        }

        protected override void Action(ArgumentsValues arguments, OptionsValues options)
        {
            var animelist = arguments.ContainsKey("user") ? MiniMALClient.LoadAnimelist((string)arguments["user"]) : Client.LoadAnimelist();

            IEnumerable<Anime> list = new List<Anime>();
            foreach (var opt in options.Keys)
                switch (opt)
                {
                    case "w": list = list.Concat(animelist[WatchingStatus.Watching]); break;
                    case "c": list = list.Concat(animelist[WatchingStatus.Completed]); break;
                    case "h": list = list.Concat(animelist[WatchingStatus.OnHold]); break;
                    case "d": list = list.Concat(animelist[WatchingStatus.Dropped]); break;
                    case "p": list = list.Concat(animelist[WatchingStatus.PlanToWatch]); break;
                }

            var enumerable = list as IList<Anime> ?? list.ToList();

            if (!enumerable.Any())
                enumerable = animelist.ToList();

            foreach (var a in enumerable)
                System.Console.WriteLine(a.Title);
            System.Console.WriteLine(enumerable.Count() + " entries");
        }
    }
}