using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace MiniMAL.API
{
	public class Client
    {
        // http://myanimelist.net/malappinfo.php?status=all&type=anime&u=Luxiamimi

        /// <summary>
        /// Load an user anime list
        /// </summary>
        /// <param name="user">Name of the user</param>
        /// <param name="type">List type : anime (by defaut)/manga)</param>
        /// <param name="status">Status for the animes get : all (by defaut)/...)</param>
		public List<Anime> LoadUserList(string user, string type = "anime", string status = "all")
		{
			List<Anime> list = new List<Anime> ();
            string s = "http://myanimelist.net/malappinfo.php?u=" + user + "&type=" + type + "&status=" + status;
            //string s = "myXmlList.xml";
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
