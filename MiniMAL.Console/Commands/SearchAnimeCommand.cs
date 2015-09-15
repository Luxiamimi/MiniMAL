using System.Collections.Generic;
using MiniMAL.Anime;
using MiniMAL.Console.Commands.Abstract;
using MiniMAL.Exceptions;
using StarLess;

namespace MiniMAL.Console.Commands
{
    internal class SearchAnimeCommand : MiniMALUnlimitedCommand
    {
        public SearchAnimeCommand(MiniMALClient client)
            : base(client, "search-anime", "Search an anime in MyAnimeList database.")
        {
            Argument = new Argument<string>("query", "query for the anime search");
        }

        protected override void Action(ArgumentsValues args, OptionsValues options)
        {
            try
            {
                List<AnimeSearchEntry> search = Client.SearchAnime(string.Join(" ", args.Values));
                foreach (AnimeSearchEntry a in search)
                    System.Console.WriteLine(a.Title);
                System.Console.WriteLine(search.Count + " entries");
            }
            catch (UserNotConnectedException e)
            {
                System.Console.WriteLine(e.Message);
            }
        }
    }
}