using System;
using System.Collections.Generic;
using System.Linq;
using Diese.ConsoleInterface;

namespace MiniMAL.ConsoleInterface.Commands
{
    public class AnimelistCommand : MiniMALCommand
    {
        public AnimelistCommand(MiniMALClient client)
            : base(client, "animelist", "Display the anime list from a user.")
        {
            RequiredArguments.Add(new Argument("user", "a MyAnimeList's username.", new Validator(s => s != "", "Username can't be empty. Exemple : animelist myUsername")));

            Options.Add(new Option("w", "watching", "Select currently watching entries."));
            Options.Add(new Option("c", "completed", "Select completed entries."));
            Options.Add(new Option("h", "hold", "Select on-hold entries."));
            Options.Add(new Option("d", "dropped", "Select dropped entries."));
            Options.Add(new Option("p", "planned", "Select plan to read entries."));
        }

        protected override void Action(ArgumentsValues arguments, OptionsValues options)
        {
            AnimeList animelist = client.LoadAnimelist(arguments["user"]);

            IEnumerable<Anime> list = new List<Anime>();
            foreach (string opt in options.Keys)
                switch (opt)
                {
                    case "w": list = list.Concat(animelist[WatchingStatus.Watching]); break;
                    case "c": list = list.Concat(animelist[WatchingStatus.Completed]); break;
                    case "h": list = list.Concat(animelist[WatchingStatus.OnHold]); break;
                    case "d": list = list.Concat(animelist[WatchingStatus.Dropped]); break;
                    case "p": list = list.Concat(animelist[WatchingStatus.PlanToWatch]); break;
                }

            if (!list.Any())
                list = animelist.ToList();

            foreach (Anime a in list)
                Console.WriteLine(a.Title);
            Console.WriteLine(list.Count() + " entries");
        }
    }
}