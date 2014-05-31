using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Diese.ConsoleInterface;
using MiniMAL;

namespace MiniMAL.ConsoleInterface.Commands
{
    public class AnimelistCommand : Command
    {
        public AnimelistCommand() : base("animelist")
        {
            Description = "Display the anime list from a user.";

            RequiredArguments.Add(new Argument("user", "a MyAnimeList's username.", new Validator(s => s != "", "Username can't be empty. Exemple : animelist myUsername")));
        }

        protected override void Action(ArgumentsDictionary arguments, OptionsDictionary options)
        {
            Console.WriteLine("Loading...");
            MiniMALClient client = new MiniMALClient();
            AnimeList list = client.LoadAnimelist(arguments["user"]);

            foreach (Anime a in list)
                Console.WriteLine(a.Title);
            Console.WriteLine(list.Count + " entries");
        }
    }
}
