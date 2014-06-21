using System;

namespace MiniMAL.Console.Commands
{
    public class LoginCommand : MiniMALCommand
    {
        public LoginCommand(MiniMALClient client)
            : base(client, "login", "Connect a user at MyAnimeList services.")
        {
        }

        protected override void Action(ArgumentsValues arguments, OptionsValues options)
        {
            System.Console.Write("Enter your username : ");

            string username = System.Console.ReadLine();

            System.Console.Write("Enter your password : ");

            string password = "";
            ConsoleKeyInfo key;
            do
            {
                key = System.Console.ReadKey(true);

                if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
                {
                    password += key.KeyChar;
                    System.Console.Write("*");
                }
                else if (key.Key == ConsoleKey.Backspace && password.Length > 0)
                {
                    password = password.Substring(0, (password.Length - 1));
                    System.Console.Write("\b \b");
                }
            }
            while (key.Key != ConsoleKey.Enter);
            System.Console.WriteLine();

            try
            {
                client.Authentification(username, password);
                System.Console.WriteLine("Success");
            }
            catch (UnauthorizedAccessException e)
            {
                System.Console.WriteLine(e.Message);
            }
        }
    }
}