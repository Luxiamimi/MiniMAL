﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MiniMAL;
using System.Xml;
using System.Net;
using MiniMAL.ConsoleInterface.Commands;

namespace MiniMAL.ConsoleInterface
{
    public class MiniMALConsole : Diese.ConsoleInterface.ConsoleInterface
    {
        private MiniMALClient client;

        public MiniMALConsole() : base("MiniMal")
        {
            client = new MiniMALClient();

            WelcomeMessage = "Welcome to MiniMal !";
            Commands.Add("login", new LoginCommand(client));
            Commands.Add("animelist", new AnimelistCommand(client));
            Commands.Add("mangalist", new MangalistCommand(client));
            Commands.Add("search-anime", new SearchAnimeCommand(client));
            Commands.Add("search-manga", new SearchMangaCommand(client));
        }
    }
}
