using System.Collections.Generic;
using System.Linq;
using MiniMAL.Exceptions;
using StarLess;

namespace MiniMAL.Console.Commands
{
    internal class SearchMangaCommand : MiniMALUnlimitedCommand
    {
        public SearchMangaCommand(MiniMALClient client)
            : base(client, "search-manga", "Search a manga in MyAnimeList database.")
        {
            Argument = new Argument("query", typeof(string), "query for the manga search");
        }

        protected override void Action(ArgumentsValues args, OptionsValues options)
        {
            try
            {
                List<MangaSearchEntry> search = Client.SearchManga(args.Values.ToArray());
                foreach (MangaSearchEntry a in search)
                    System.Console.WriteLine(a.Title);
                System.Console.WriteLine(search.Count() + " entries");
            }
            catch (UserNotConnectedException e)
            {
                System.Console.WriteLine(e.Message);
            }
        }
    }
}