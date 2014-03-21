using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Diese.ConsoleInterface;

namespace Diese.ConsoleInterface
{
    public abstract class Command
    {
        public string Keyword { get; set; }
        public List<Argument> Arguments { get; set; }

        protected Command(string keyword)
        {
            Keyword = keyword;
            Arguments = new List<Argument>();
        }

        public void Run(List<string> args)
        {
            int i = 0;
            foreach (Argument a in Arguments)
            {
                if (i > Arguments.Count)
                {
                    Console.WriteLine("ERROR : number of arguments invalid !");
                    return;
                }

                if((!a.isValid(args[i]) && !a.isOptional && i >= Arguments.Count))
                {
                    Console.WriteLine("ERROR : " + args[i] + " is not a valid argument !");
                    return;
                }
                i++;
            }
            Action(args);
        }

        protected abstract void Action(List<string> args);
    }
}
