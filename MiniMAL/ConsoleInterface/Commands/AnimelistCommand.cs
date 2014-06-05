using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Diese.ConsoleInterface;
using MiniMAL;

namespace MiniMAL.ConsoleInterface.Commands
{
    public class AnimelistCommand : Command
    {
        public AnimelistCommand() : base("animelist")
        {
            Description = "Display the anime list from a user.";

            RequiredArguments.Add(new Argument("user", "a MyAnimeList's username.", new Validator(s => s != "", "Username can't be empty. Exemple : animelist myUsername")));

            Options.Add("-w", new Option("-w", "currently watching"));
            Options.Add("-c", new Option("-c", "completed"));
            Options.Add("-h", new Option("-h", "on-hold"));
            Options.Add("-d", new Option("-d", "dropped"));
            Options.Add("-p", new Option("-p", "plan to watch"));
        }

        protected override void Action(ArgumentsDictionary arguments, OptionsDictionary options)
        {
            Console.WriteLine("Loading...");
            MiniMALClient client = new MiniMALClient();
            AnimeList animelist = client.LoadAnimelist(arguments["user"]);

            IEnumerable<Anime> list = new List<Anime>();
            foreach (string opt in options.Keys)
                switch (opt)
                {
                    case "-w": list = list.Concat(animelist[WatchingStatus.Watching]); break;
                    case "-c": list = list.Concat(animelist[WatchingStatus.Completed]); break;
                    case "-h": list = list.Concat(animelist[WatchingStatus.OnHold]); break;
                    case "-d": list = list.Concat(animelist[WatchingStatus.Dropped]); break;
                    case "-p": list = list.Concat(animelist[WatchingStatus.PlanToWatch]); break;
                }

            if (!list.Any())
                list = animelist.ToList();

            foreach (Anime a in list)
                Console.WriteLine(a.Title);
            Console.WriteLine(list.Count() + " entries");
        }
    }
}
