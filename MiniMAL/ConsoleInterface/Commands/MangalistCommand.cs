using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Diese.ConsoleInterface;
using MiniMAL;

namespace MiniMAL.ConsoleInterface.Commands
{
    public class MangalistCommand : MiniMALCommand
    {
        public MangalistCommand(MiniMALClient client) : base(client, "mangalist")
        {
            Description = "Display the manga list from a user.";

            RequiredArguments.Add(new Argument("user", "a MyAnimeList's username.", new Validator(s => s != "", "Username can't be empty. Exemple : mangalist myUsername")));

            Options.Add("-r", new Option("-r", "currently reading"));
            Options.Add("-c", new Option("-c", "completed"));
            Options.Add("-h", new Option("-h", "on-hold"));
            Options.Add("-d", new Option("-d", "dropped"));
            Options.Add("-p", new Option("-p", "plan to read"));
        }

        protected override void Action(ArgumentsDictionary arguments, OptionsDictionary options)
        {
            MangaList mangalist = client.LoadMangalist(arguments["user"]);

            IEnumerable<Manga> list = new List<Manga>();
            foreach (string opt in options.Keys)
                switch (opt)
                {
                    case "-r": list = list.Concat(mangalist[ReadingStatus.Reading]); break;
                    case "-c": list = list.Concat(mangalist[ReadingStatus.Completed]); break;
                    case "-h": list = list.Concat(mangalist[ReadingStatus.OnHold]); break;
                    case "-d": list = list.Concat(mangalist[ReadingStatus.Dropped]); break;
                    case "-p": list = list.Concat(mangalist[ReadingStatus.PlanToRead]); break;
                }

            if (!list.Any())
                list = mangalist.ToList();

            foreach (Manga m in list)
                Console.WriteLine(m.Title);
            Console.WriteLine(list.Count() + " entries");
        }
    }
}
