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
    }
}
