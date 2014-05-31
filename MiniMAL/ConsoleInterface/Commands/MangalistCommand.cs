using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Diese.ConsoleInterface;
using MiniMAL;

namespace MiniMAL.ConsoleInterface.Commands
{
    public class MangalistCommand : Command
    {
        public MangalistCommand() : base("mangalist")
        {
            Description = "Display the manga list from a user.";

            RequiredArguments.Add(new Argument("user", "a MyAnimeList's username.", new Validator(s => s != "", "Username can't be empty. Exemple : mangalist myUsername")));
        }

        protected override void Action(string[] args)
        {
            Console.WriteLine("Loading...");
            MiniMALClient client = new MiniMALClient();
            MangaList list = client.LoadMangalist(args[0]);

            foreach (Manga m in list)
                Console.WriteLine(m.Title);
            Console.WriteLine(list.Count + " entries");
        }
    }
}
