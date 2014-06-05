using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml;

namespace MiniMAL
{
	public class MiniMALClient
    {
        // http://myanimelist.net/malappinfo.php?status=all&type=anime&u=Luxiamimi
        // http://myanimelist.net/malappinfo.php?status=all&type=manga&u=Aeden

        private string Username;
        private string Password;

        public bool TryAuthentification(string username, string password, out string error)
        {
            string link = "http://myanimelist.net/api/account/verify_credentials.xml";
            WebRequest request = WebRequest.Create(link);
            request.Credentials = new NetworkCredential(username, password);

            try
            {
                WebResponse response = request.GetResponse();
            }
            catch (WebException e)
            {
                error = e.Message;
                return false;
            }

            Username = username;
            Password = password;

            error = "";
            return true;
        }

        public AnimeList LoadAnimelist(string user)
		{
            AnimeList list = new AnimeList();

            string link = "http://myanimelist.net/malappinfo.php?u=" + user + "&type=anime&status=all";
            XmlDocument xml = LoadXML(link);

            foreach (XmlNode e in xml.DocumentElement.ChildNodes)
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

        public MangaList LoadMangalist(string user)
        {
            MangaList list = new MangaList();

            string link = "http://myanimelist.net/malappinfo.php?u=" + user + "&type=manga&status=all";
            XmlDocument xml = LoadXML(link);

            foreach (XmlNode e in xml.DocumentElement.ChildNodes)
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

        public void SearchAnime(string[] search)
        {
            string link = "http://myanimelist.net/api/anime/search.xml?q=";

            if (search.Any())
                link += search[0];

            for (int i = 1; i < search.Length; i++)
                link += "+" + search[i];

            //XmlDocument xml = LoadXML(link);

            XmlDocument xml = LoadXMLwithCredentials(link);

            foreach (XmlNode e in xml.DocumentElement.ChildNodes)
            {
                if (e.Name == "entry")
                {
                    Manga m = new Manga();
                    m.LoadFromXmlNode(e);
                    list.Add(m);
                }
            }

            return;
        }

        private XmlDocument LoadXML(string link)
        {
            XmlDocument xml = new XmlDocument();
            xml.Load(link);
            return xml;
        }

        private XmlDocument LoadXMLwithCredentials(string link)
        {
            WebRequest request = WebRequest.Create(link);
            request.Credentials = new NetworkCredential(Username, Password);

            XmlDocument xml = new XmlDocument();
            xml.Load(request.GetResponse().GetResponseStream());
            return xml;
        }
    }
}
