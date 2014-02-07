using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MiniMAL.API;

namespace MiniMAL
{
    public class MiniMALConsole
    {
        private Client client = new Client();

        public MiniMALConsole()
        {
        }

        public bool Run(string[] args)
        {
            switch (args[0])
            {
                case "exit":
                    return false;
                case "animelist":
                    Console.WriteLine("Loading...");
                    List<Anime> animelist = client.LoadUserList(args[1], Client.ListType.Anime);
                    Console.WriteLine(args[1] + "\'s anime list");
                    foreach (Anime a in animelist)
                        Console.WriteLine(a.Title);
                    Console.WriteLine(animelist.Count + " entries");
                    break;
            }
            return true;
        }
    }
}
