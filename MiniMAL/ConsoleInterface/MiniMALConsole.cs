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

            AddCommand(new LoginCommand(client));
            AddCommand(new AnimelistCommand(client));
            AddCommand(new MangalistCommand(client));
            AddCommand(new SearchAnimeCommand(client));
            AddCommand(new SearchMangaCommand(client));
        }
    }
}
