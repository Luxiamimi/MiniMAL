using System.Linq;

namespace MiniMALConsole
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
