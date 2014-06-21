using System.Linq;

namespace MiniMAL.Console
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            MiniMALConsole console = new MiniMALConsole();

            if (args.Any())
                console.Request(args);
            else
                console.Run();
        }
    }
}