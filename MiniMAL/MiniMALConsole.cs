﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MiniMAL.API;
using System.Xml;
using System.Net;

namespace MiniMAL
{
    public class MiniMALConsole
    {
        private Client client = new Client();

        public MiniMALConsole()
        {
        }

        public bool Run(string[] args)
        {
            switch (args[0])
            {
                case "exit":
                    return false;
                case "animelist":
                    Console.WriteLine("Loading...");
					try
					{
						List<Anime> animelist = client.LoadUserList(args[1], Client.ListType.Anime);
						Console.WriteLine(args[1] + "\'s anime list");
						foreach (Anime a in animelist)
							Console.WriteLine(a.Title);
						Console.WriteLine(animelist.Count + " entries");
					}
				catch (XmlException) {
					Console.WriteLine("ERROR : Xml invalide !");
					}
				catch (WebException) {
					Console.WriteLine("ERROR : Accès web éronné !");
					}
                    break;
            }
            return true;
        }
    }
}
