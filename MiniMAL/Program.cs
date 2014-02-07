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
            Client client = new Client();
            List<Anime>client.LoadUserList("Luxiamimi");
        }
    }
}
