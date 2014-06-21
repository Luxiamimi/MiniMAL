﻿using MiniMAL.Console.Commands;

namespace MiniMAL.Console
{
    public class MiniMALConsole : StarLess.ConsoleInterface
    {
        private MiniMALClient _client;

        public MiniMALConsole()
            : base("MiniMal")
        {
            _client = new MiniMALClient();

            WelcomeMessage = "Welcome to MiniMal !";

            AddCommand(new LoginCommand(_client));
            AddCommand(new AnimelistCommand(_client));
            AddCommand(new MangalistCommand(_client));
            AddCommand(new SearchAnimeCommand(_client));
            AddCommand(new SearchMangaCommand(_client));
        }
    }
}