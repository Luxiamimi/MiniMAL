﻿using System;
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

            Arguments.Add(new Argument(s => s != "", "Username can't be empty"));
        }

        protected override void Action(string[] args)
        {
            Console.WriteLine("Loading...");
            Client client = new Client();
            List<Anime> list = client.LoadUserList(args[0]);

            foreach (Anime a in list)
                Console.WriteLine(a.Title);
            Console.WriteLine(list.Count + " entries");
        }
    }
}
