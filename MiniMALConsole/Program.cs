using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniMALConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            MiniMAL.ConsoleInterface.MiniMALConsole console = new MiniMAL.ConsoleInterface.MiniMALConsole();

            if (args.Any())
                console.Request(args);
            else
                console.Run();
        }
    }
}
