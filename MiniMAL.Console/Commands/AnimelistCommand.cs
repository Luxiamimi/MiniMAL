using System.Collections.Generic;
using System.Linq;
using MiniMAL.Anime;
using MiniMAL.Console.Commands.Abstract;
using StarLess;

namespace MiniMAL.Console.Commands
{
    internal class AnimelistCommand : MiniMALCommand
    {
        public AnimelistCommand(MiniMALClient client)
            : base(client, "animelist", "Display the anime list from a user.")
        {
            OptionalArguments.Add(new Argument<string>("user",
                "a MyAnimeList's username. (connected user if not stated)"));

            Options.Add(new Option("w", "watching", "Select currently watching entries."));
            Options.Add(new Option("c", "completed", "Select completed entries."));
            Options.Add(new Option("h", "hold", "Select on-hold entries."));
            Options.Add(new Option("d", "dropped", "Select dropped entries."));
            Options.Add(new Option("p", "planned", "Select plan to read entries."));
        }

        protected override void Action(ArgumentsValues args, OptionsValues options)
        {
            AnimeList animelist = args.ContainsKey("user")
                ? MiniMALClient.LoadAnimelist(args.Value<string>("user"))
                : Client.LoadAnimelist();

            IEnumerable<Anime.Anime> list = new List<Anime.Anime>();
            foreach (Option.OptionKeys opt in options.Keys)
                switch (opt.Long)
                {
                    case "watching":
                        list = list.Concat(animelist[WatchingStatus.Watching]);
                        break;
                    case "completed":
                        list = list.Concat(animelist[WatchingStatus.Completed]);
                        break;
                    case "hold":
                        list = list.Concat(animelist[WatchingStatus.OnHold]);
                        break;
                    case "dropped":
                        list = list.Concat(animelist[WatchingStatus.Dropped]);
                        break;
                    case "planned":
                        list = list.Concat(animelist[WatchingStatus.PlanToWatch]);
                        break;
                }

            IList<Anime.Anime> enumerable = list as IList<Anime.Anime> ?? list.ToList();

            if (!enumerable.Any())
                enumerable = animelist.ToList();

            System.Console.WriteLine();
            foreach (Anime.Anime a in enumerable)
                System.Console.WriteLine("({0}) {1}", a.Id, a.Title);
            System.Console.WriteLine();
            System.Console.WriteLine(enumerable.Count + " entries");
        }
    }
}