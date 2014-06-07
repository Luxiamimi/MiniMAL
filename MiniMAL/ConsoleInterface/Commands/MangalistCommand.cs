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

            Options.Add(new Option("r", "reading", "Select currently reading entries."));
            Options.Add(new Option("c", "completed", "Select completed entries."));
            Options.Add(new Option("h", "hold", "Select on-hold entries."));
            Options.Add(new Option("d", "dropped", "Select dropped entries."));
            Options.Add(new Option("p", "planned", "Select plan to read entries."));
        }

        protected override void Action(ArgumentsValues arguments, OptionsValues options)
        {
            MangaList mangalist = client.LoadMangalist(arguments["user"]);

            IEnumerable<Manga> list = new List<Manga>();
            foreach (string opt in options.Keys)
                switch (opt)
                {
                    case "r": list = list.Concat(mangalist[ReadingStatus.Reading]); break;
                    case "c": list = list.Concat(mangalist[ReadingStatus.Completed]); break;
                    case "h": list = list.Concat(mangalist[ReadingStatus.OnHold]); break;
                    case "d": list = list.Concat(mangalist[ReadingStatus.Dropped]); break;
                    case "p": list = list.Concat(mangalist[ReadingStatus.PlanToRead]); break;
                }

            if (!list.Any())
                list = mangalist.ToList();

            foreach (Manga m in list)
                Console.WriteLine(m.Title);
            Console.WriteLine(list.Count() + " entries");
        }
    }
}
