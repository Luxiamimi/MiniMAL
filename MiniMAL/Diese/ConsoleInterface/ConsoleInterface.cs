﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Diese.ConsoleInterface.Exceptions;

namespace Diese.ConsoleInterface
{
    public abstract class ConsoleInterface
    {
        public string Name { get; set; }
        public string WelcomeMessage { get; set; }

        public Dictionary<string, Command> Commands { get; private set; }

        private static string exitKeyword = "exit";
        private static string helpKeyword = "help";

        protected ConsoleInterface(string name)
        {
            Name = name;

            Commands = new Dictionary<string, Command>();
            Commands.Add(exitKeyword, new ExitCommand(name));
            Commands.Add(helpKeyword, new HelpCommand(Commands));
        }

        public void AddCommand(Command c)
        {
            Commands.Add(c.Keyword, c);
        }

        public void Run()
        {
            WriteWelcomeMessage();
            while (WaitRequest()) { }
        }

        public bool WaitRequest()
        {
            Console.WriteLine();
            Console.Write(Name + ">");
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
            try
            {
                if (request.Command == "")
                    return true;

                if (request.Command == exitKeyword)
                {
                    Commands[exitKeyword].Run(request.Arguments);
                    return false;
                }

                if (!Commands.Keys.Contains(request.Command))
                    throw new UnknownCommandException(request.Command);

                Commands[request.Command].Run(request.Arguments);
            }
            catch (ConsoleInterfaceException e)
            {
                Console.WriteLine(e.Message);
            }

            return true;
        }
    }
}
