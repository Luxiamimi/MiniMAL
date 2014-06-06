using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Diese.ConsoleInterface;
using MiniMAL;

namespace MiniMAL.ConsoleInterface.Commands
{
    public class LoginCommand : MiniMALCommand
    {
        public LoginCommand(MiniMALClient client) : base(client, "login")
        {
            Description = "Connect a user at MyAnimeList services.";
        }

        protected override void Action(ArgumentsDictionary arguments, OptionsDictionary options)
        {
            Console.Write("Enter your username : ");

            string username = Console.ReadLine();

            Console.Write("Enter your password : ");

            string password = "";
            ConsoleKeyInfo key;
            do
            {
                key = Console.ReadKey(true);

                if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
                {
                    password += key.KeyChar;
                   Console.Write("*");
                }
                else if (key.Key == ConsoleKey.Backspace && password.Length > 0)
                {
                    password = password.Substring(0, (password.Length - 1));
                    Console.Write("\b \b");
                }
            }
            while (key.Key != ConsoleKey.Enter);
            Console.WriteLine();

            try
            {
                client.Authentification(username, password);
                Console.WriteLine("Success");
            }
            catch (UnauthorizedAccessException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
