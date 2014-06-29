using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml;
using MiniMAL.Exceptions;

namespace MiniMAL
{
    // TODO : add anime/edit a list
    // TODO : use string.Format()
    public class MiniMALClient
    {
        public bool IsConnected { get; private set; }
        public MiniMALClientData ClientData { get; private set; }

        private static string configFilename = "configMiniMAL.xml";

        public MiniMALClient()
        {
            IsConnected = false;
        }

        public void SaveConfig()
        {
            MiniMALClientData.Save(ClientData, configFilename);
        }

        public void LoadConfig()
        {
            try
            {
                ClientData = MiniMALClientData.Load(configFilename);
            }
            catch (FileNotFoundException)
            {
                throw new ConfigFileNotFoundException();
            }
            catch (InvalidOperationException)
            {
                throw new ConfigFileCorruptException();
            }

            Authentification(ClientData.Username, ClientData.DecryptedPassword);
        }

        public void Authentification(string username, string password)
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
                IsConnected = false;
                HttpWebResponse error = (HttpWebResponse)e.Response;

                if (error.StatusCode == HttpStatusCode.Unauthorized)
                    throw new UserUnauthorizedException();
                else
                    throw e;
            }

            ClientData = new MiniMALClientData(username, password);
            IsConnected = true;
        }

        public AnimeList LoadAnimelist()
        {
            if (IsConnected)
                return LoadAnimelist(ClientData.Username);
            else
                throw new UserNotConnectedException();
        }

        public AnimeList LoadAnimelist(string user = "")
        {
            AnimeList list = new AnimeList();

            string link = "http://myanimelist.net/malappinfo.php?u=" + user + "&type=anime&status=all";
            XmlDocument xml = LoadXml(link);

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

        public bool AddAnime(int id, AnimeRequestData data)
        {
            string link = "http://myanimelist.net/api/animelist/add/" + id + ".xml";

            Dictionary<string, string> requestData = new Dictionary<string, string>();
            requestData.Add("data", data.SerializeToString());

            HttpWebResponse response;
            LoadXmlWithCredentials(link, "POST", requestData, out response);

            return true;
        }

        public MangaList LoadMangalist()
        {
            if (IsConnected)
                return LoadMangalist(ClientData.Username);
            else
                throw new UserNotConnectedException();
        }

        public MangaList LoadMangalist(string user = "")
        {
            MangaList list = new MangaList();

            string link = "http://myanimelist.net/malappinfo.php?u=" + user + "&type=manga&status=all";
            XmlDocument xml = LoadXml(link);

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

        public List<AnimeSearchEntry> SearchAnime(string[] search)
        {
            string link = "http://myanimelist.net/api/anime/search.xml?q=";

            if (search.Any())
                link += search[0];

            for (int i = 1; i < search.Length; i++)
                link += "+" + search[i];

            XmlDocument xml = LoadXmlWithCredentials(link, "GET");

            List<AnimeSearchEntry> list = new List<AnimeSearchEntry>();
            foreach (XmlNode e in xml.DocumentElement.ChildNodes)
            {
                if (e.Name == "entry")
                {
                    AnimeSearchEntry a = new AnimeSearchEntry();
                    a.LoadFromXmlNode(e);
                    list.Add(a);
                }
            }

            return list;
        }

        public List<MangaSearchEntry> SearchManga(string[] search)
        {
            string link = "http://myanimelist.net/api/manga/search.xml?q=";

            if (search.Any())
                link += search[0];

            for (int i = 1; i < search.Length; i++)
                link += "+" + search[i];

            XmlDocument xml = LoadXmlWithCredentials(link, "GET");

            List<MangaSearchEntry> list = new List<MangaSearchEntry>();
            foreach (XmlNode e in xml.DocumentElement.ChildNodes)
            {
                if (e.Name == "entry")
                {
                    MangaSearchEntry m = new MangaSearchEntry();
                    m.LoadFromXmlNode(e);
                    list.Add(m);
                }
            }

            return list;
        }

        private XmlDocument LoadXml(string link)
        {
            XmlDocument xml = new XmlDocument();
            xml.Load(link);
            return xml;
        }

        private XmlDocument LoadXmlWithCredentials(string link, string method)
        {
            return LoadXmlWithCredentials(link, method, new Dictionary<string, string>());
        }

        private XmlDocument LoadXmlWithCredentials(string link, string method, Dictionary<string, string> data)
        {
            HttpWebResponse response;
            return LoadXmlWithCredentials(link, method, data, out response);
        }

        private XmlDocument LoadXmlWithCredentials(string link, string method, Dictionary<string, string> data, out HttpWebResponse response)
        {
            if (!IsConnected)
                throw new UserNotConnectedException();

            if (data.Any())
            {
                link += "?";
                foreach (KeyValuePair<string, string> s in data)
                    link += s.Key + "=" + s.Value;
            }

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(link);
            request.Credentials = new NetworkCredential(ClientData.Username, ClientData.DecryptedPassword);
            request.PreAuthenticate = true;
            request.Timeout = 10 * 1000;

            try
            {
                response = (HttpWebResponse)request.GetResponse();
            }
            catch (WebException e)
            {
                HttpWebResponse error = (HttpWebResponse)e.Response;
                StreamReader readStream = new StreamReader(error.GetResponseStream(), Encoding.UTF8);
                Console.WriteLine(readStream.ReadToEnd());

                readStream.Close();
                throw e;
            }

            if (response.ContentType.Contains("xml"))
            {
                XmlDocument xml = new XmlDocument();
                StreamReader sr = new StreamReader(response.GetResponseStream());
                string content = sr.ReadToEnd().Replace("&mdash;", "&#8212;").Replace("&forall;", "&#8704;");
                xml.LoadXml(content);

                response.Close();
                return xml;
            }
            else
                return new XmlDocument();
        }
    }
}