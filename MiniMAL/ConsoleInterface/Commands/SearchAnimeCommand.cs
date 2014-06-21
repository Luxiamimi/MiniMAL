using System;
using System.Collections.Generic;
using System.Linq;
using MiniMAL.Exceptions;

namespace MiniMAL.ConsoleInterface.Commands
{
    public class SearchAnimeCommand : MiniMALCommand
    {
        public SearchAnimeCommand(MiniMALClient client)
            : base(client, "search-anime", "Search an anime in MyAnimeList database.")
        {
            UnlimitedArguments = true;
        }

        protected override void Action(ArgumentsValues arguments, OptionsValues options)
        {
            try
            {
                List<AnimeSearchEntry> search = client.SearchAnime(arguments.Values.ToArray());
                foreach (AnimeSearchEntry a in search)
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