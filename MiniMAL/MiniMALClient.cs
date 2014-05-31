using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace MiniMAL
{
	public class MiniMALClient
    {
        // http://myanimelist.net/malappinfo.php?status=all&type=anime&u=Luxiamimi
        // http://myanimelist.net/malappinfo.php?status=all&type=manga&u=Aeden

        public List<Anime> LoadAnimelist(string user)
		{
            List<Anime> list = new List<Anime>();

            string s = "http://myanimelist.net/malappinfo.php?u=" + user + "&type=anime&status=all";
			XmlDocument doc = new XmlDocument();
			doc.Load(s);

			foreach (XmlNode e in doc.DocumentElement.ChildNodes)
            {
                if (e.Name == "anime")
				{
                    Anime a = new Anime();
					a.LoadFromXmlNode(e);
					list.Add(a);
				}
			}
			return list;
        }

        public List<Manga> LoadMangalist(string user)
        {
            List<Manga> list = new List<Manga>();

            string s = "http://myanimelist.net/malappinfo.php?u=" + user + "&type=manga&status=all";
            XmlDocument doc = new XmlDocument();
            doc.Load(s);

            foreach (XmlNode e in doc.DocumentElement.ChildNodes)
            {
                if (e.Name == "manga")
                {
                    Manga m = new Manga();
                    m.LoadFromXmlNode(e);
                    list.Add(m);
                }
            }
            return list;
        }
    }
}
