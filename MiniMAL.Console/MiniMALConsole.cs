﻿﻿using System;
using MiniMAL.Console.Commands;
using MiniMAL.Exceptions;

namespace MiniMAL.Console
{
    // TODO : Add a resource file for translation
    public class MiniMALConsole : StarLess.ConsoleInterface
    {
        private readonly MiniMALClient _client;

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
            AddCommand(new AddAnimeCommand(_client));
            AddCommand(new AddMangaCommand(_client));
        }

        protected override void Initialize()
        {
            try
            {
                System.Console.WriteLine("Load configuration...");
                _client.LoadConfig();
                System.Console.WriteLine("Connected as " + _client.ClientData.Username);
            }
            catch (Exception e)
            {
                System.Console.WriteLine();
                System.Console.WriteLine(e.Message);
                if (e is ConfigFileNotFoundException)
                {
                    System.Console.WriteLine("Please enter your MyAnimeList's credentials.");
                }
                else if (e is ConfigFileCorruptException)
                {
                    System.Console.WriteLine("Please enter your MyAnimeList's credentials.");
                }
                System.Console.WriteLine();

                while (true)
                {
                    try
                    {
                        Commands["login"].Run();
                        break;
                    }
                    catch (Exception ee)
                    {
                        System.Console.WriteLine(ee.Message + "\n");
                    }
                }
            }
        }
    }
}