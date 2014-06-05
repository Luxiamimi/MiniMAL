﻿﻿using System;
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
        public MiniMALConsole() : base("MiniMal")
        {
            WelcomeMessage = "Welcome to MiniMal !";
            Commands.Add("animelist", new AnimelistCommand());
            Commands.Add("mangalist", new MangalistCommand());
            Commands.Add("login", new LoginCommand());
        }
    }
}
