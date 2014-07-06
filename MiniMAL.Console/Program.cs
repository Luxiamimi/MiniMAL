using System.Linq;

namespace MiniMAL.Console
{
    static class Program
    {
        static private void Main(string[] args)
        {
            var console = new MiniMALConsole();

            if (args.Any())
                console.Request(args);
            else
                console.Run();
        }
    }
}