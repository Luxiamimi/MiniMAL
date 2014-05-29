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
            //Console.WriteLine("Loading...");
            //MiniMALClient client = new MiniMALClient();
            //List<Anime> list = client.LoadMangalist(args[0]);

            //foreach (Anime a in list)
            //    Console.WriteLine(a.Title);
            //Console.WriteLine(list.Count + " entries");
        }
    }
}
