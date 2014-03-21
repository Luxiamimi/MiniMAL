using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MiniMAL.ConsoleInterface;

namespace Program
{
    class Program
    {
        static void Main(string[] args)
        {
            MiniMALConsole console = new MiniMALConsole();

            if (args.Any())
                console.Request(args);
            else
                console.Run();
        }
    }
}
