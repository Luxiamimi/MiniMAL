using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Diese.ConsoleInterface;
using MiniMAL;

namespace MiniMAL.ConsoleInterface.Commands
{
    public class SearchCommand : Command
    {
        public SearchCommand() : base("search")
        {
            Description = "Search an anime in MyAnimeList database.";
        }

        protected override void Action(ArgumentsDictionary arguments, OptionsDictionary options)
        {
            Console.WriteLine("Loading...");
            MiniMALClient client = new MiniMALClient();
            List<AnimeSearchEntry> search = client.SearchAnime(new string[] { "full", "metal" });

            foreach (AnimeSearchEntry a in search)
                Console.WriteLine(a.Title);
            Console.WriteLine(search.Count() + " entries");
        }
    }
}
