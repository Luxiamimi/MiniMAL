﻿using System;
using MiniMAL.Console.Commands;

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

        protected override void Initialize()
        {
            try
            {
                System.Console.WriteLine("Load config...");
                _client.LoadConfig();
                System.Console.WriteLine("Connected as " + _client.Username);
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
            }
        }
    }
}