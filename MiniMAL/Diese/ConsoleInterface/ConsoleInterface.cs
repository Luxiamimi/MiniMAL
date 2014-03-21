using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diese.ConsoleInterface
{
    public abstract class ConsoleInterface
    {
        public string Name { get; set; }
        public string WelcomeMessage { get; set; }

        public Dictionary<string, Command> Commands { get; set; }
        public Command ExitCommand { get; set; }

        protected ConsoleInterface()
        {
            Commands = new Dictionary<string, Command>();
            ExitCommand = new ExitCommand();
        }

        public void Run()
        {
            WriteWelcomeMessage();
            while (WaitRequest()) { }
        }

        public bool WaitRequest()
        {
            Console.Write(Name + "> ");
            return Request(Console.ReadLine().Split(new char[] { ' ' }));
        }

        public void WriteWelcomeMessage()
        {
            Console.WriteLine(WelcomeMessage);
        }

        public bool Request(string[] request)
        {
            return Request(new Request(request));
        }

        public bool Request(Request request)
        {
            if (request.Keyword == "")
                return true;
            if (request.Keyword == ExitCommand.Keyword)
            {
                ExitCommand.Run(request.Arguments);
                return false;
            }

            if (!Commands.Keys.Contains(request.Keyword))
            {
                Console.WriteLine("ERROR : " + request.Keyword + " is an invalid command !");
                return true;
            }

            Commands[request.Keyword].Run(request.Arguments);
            return true;
        }
    }
}
