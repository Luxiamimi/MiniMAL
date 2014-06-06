using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Diese.ConsoleInterface;
using MiniMAL;
using MiniMAL.Exceptions;

namespace MiniMAL.ConsoleInterface.Commands
{
    public class SearchCommand : MiniMALCommand
    {
        public SearchCommand(MiniMALClient client) : base(client, "search")
        {
            Description = "Search an anime in MyAnimeList database.";

            UnlimitedArguments = true;
        }

        protected override void Action(ArgumentsDictionary arguments, OptionsDictionary options)
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
