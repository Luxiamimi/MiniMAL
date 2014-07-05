﻿using StarLess;

namespace MiniMAL.Console.Commands
{
    public abstract class MiniMALUnlimitedCommand : UnlimitedCommand
    {
        protected readonly MiniMALClient Client;

        protected MiniMALUnlimitedCommand(MiniMALClient client, string keyword, string description)
            : base(keyword, description)
        {
            Client = client;
        }
    }
}