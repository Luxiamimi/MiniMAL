using System.Collections.Generic;
using System.Linq;
using MiniMAL.Exceptions;
using StarLess;

namespace MiniMAL.Console.Commands
{
    public class SearchAnimeCommand : MiniMALUnlimitedCommand
    {
        public SearchAnimeCommand(MiniMALClient client)
            : base(client, "search-anime", "Search an anime in MyAnimeList database.")
        {
            Argument = new Argument("query", typeof(string), "query for the anime search");
        }

        protected override void Action(ArgumentsValues arguments, OptionsValues options)
        {
            try
            {
                List<AnimeSearchEntry> search = Client.SearchAnime(arguments.Values.ToArray());
                foreach (AnimeSearchEntry a in search)
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