using System.Collections.Generic;
using System.Linq;
using StarLess;

namespace MiniMAL.Console.Commands
{
    internal class MangalistCommand : MiniMALCommand
    {
        public MangalistCommand(MiniMALClient client)
            : base(client, "mangalist", "Display the manga list from a user.")
        {
            OptionalArguments.Add(new Argument<string>("user",
                "a MyAnimeList's username. (connected user if not stated)"));

            Options.Add(new Option("r", "reading", "Select currently reading entries."));
            Options.Add(new Option("c", "completed", "Select completed entries."));
            Options.Add(new Option("h", "hold", "Select on-hold entries."));
            Options.Add(new Option("d", "dropped", "Select dropped entries."));
            Options.Add(new Option("p", "planned", "Select plan to read entries."));
        }

        protected override void Action(ArgumentsValues args, OptionsValues options)
        {
            MangaList mangalist = args.ContainsKey("user")
                                      ? MiniMALClient.LoadMangalist(args.Value<string>("user"))
                                      : Client.LoadMangalist();

            IEnumerable<Manga> list = new List<Manga>();
            foreach (Option.OptionKeys opt in options.Keys)
                switch (opt.Long)
                {
                    case "reading":
                        list = list.Concat(mangalist[ReadingStatus.Reading]);
                        break;
                    case "completed":
                        list = list.Concat(mangalist[ReadingStatus.Completed]);
                        break;
                    case "hold":
                        list = list.Concat(mangalist[ReadingStatus.OnHold]);
                        break;
                    case "dropped":
                        list = list.Concat(mangalist[ReadingStatus.Dropped]);
                        break;
                    case "planned":
                        list = list.Concat(mangalist[ReadingStatus.PlanToRead]);
                        break;
                }

            IList<Manga> enumerable = list as IList<Manga> ?? list.ToList();

            if (!enumerable.Any())
                enumerable = mangalist.ToList();

            System.Console.WriteLine();
            foreach (Manga m in enumerable)
                System.Console.WriteLine("({0}) {1}", m.Id, m.Title);
            System.Console.WriteLine();
            System.Console.WriteLine(enumerable.Count() + " entries");
        }
    }
}