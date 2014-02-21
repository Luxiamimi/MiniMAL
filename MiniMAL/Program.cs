using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MiniMAL.API;

namespace MiniMAL
{
    class Program
    {
        static void Main(string[] args)
        {
            MiniMALConsole console = new MiniMALConsole();
            if (args.Any())
                console.Run(args);
            else
            {
                bool b = true;
                while (b)
                {
                    Console.Write("MiniMAL >");
					b = console.Run(Console.ReadLine().Split(new char[] { ' ' }));
                }
            }
        }
    }
}
