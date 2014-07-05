using System.Linq;

namespace MiniMAL.Console
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            var console = new MiniMALConsole();

            if (args.Any())
                console.Request(args);
            else
                console.Run();
        }
    }
}