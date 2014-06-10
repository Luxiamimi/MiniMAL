using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Diese.ConsoleInterface;
using MiniMAL;
using MiniMAL.Exceptions;

namespace MiniMAL.ConsoleInterface.Commands
{
    public class SearchMangaCommand : MiniMALCommand
    {
        public SearchMangaCommand(MiniMALClient client)
            : base(client, "search-manga", "Search a manga in MyAnimeList database.")
        {
            UnlimitedArguments = true;
        }

        protected override void Action(ArgumentsValues arguments, OptionsValues options)
        {
            try
            {
                List<MangaSearchEntry> search = client.SearchManga(arguments.Values.ToArray());
                foreach (MangaSearchEntry a in search)
                    Console.WriteLine(a.Title);
                Console.WriteLine(search.Count() + " entries");
            }
            catch (UserNotConnectedException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
