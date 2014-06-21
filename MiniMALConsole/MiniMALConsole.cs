﻿using MiniMAL.Console.Commands;

namespace MiniMAL.Console
{
    public class MiniMALConsole : StarLess.ConsoleInterface
    {
        private MiniMALClient client;

        public MiniMALConsole()
            : base("MiniMal")
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