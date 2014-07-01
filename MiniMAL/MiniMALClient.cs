using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Xml;
using MiniMAL.Exceptions;

namespace MiniMAL
{
    // TODO : update/edit a list
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

        public AnimeList LoadAnimelist(string user)
        {
            AnimeList list = new AnimeList();

            string link = string.Format("http://myanimelist.net/malappinfo.php?u={0}&type=anime&status=all", user);
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
            string link = string.Format("http://myanimelist.net/api/animelist/add/{0}.xml", id);

            Dictionary<string, string> requestData = new Dictionary<string, string>();
            requestData.Add("data", data.SerializeToString());

            HttpWebResponse response;
            Request(link, requestData, out response);

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

            string link = string.Format("http://myanimelist.net/malappinfo.php?u={0}&type=manga&status=all", user);
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

        public bool AddManga(int id, MangaRequestData data)
        {
            string link = string.Format("http://myanimelist.net/api/mangalist/add/{0}.xml", id);

            Dictionary<string, string> requestData = new Dictionary<string, string>();
            requestData.Add("data", data.SerializeToString());

            HttpWebResponse response;
            Request(link, requestData, out response);

            return true;
        }

        public List<AnimeSearchEntry> SearchAnime(string[] search)
        {
            List<AnimeSearchEntry> list = new List<AnimeSearchEntry>();

            string link = "http://myanimelist.net/api/anime/search.xml?q=";

            if (search.Any())
            {
                link += search[0];
                for (int i = 1; i < search.Length; i++)
                    link += "+" + search[i];
            }
            else return list;

            XmlDocument xml = RequestXml(link);

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
            List<MangaSearchEntry> list = new List<MangaSearchEntry>();

            string link = "http://myanimelist.net/api/manga/search.xml?q=";

            if (search.Any())
            {
                link += search[0];
                for (int i = 1; i < search.Length; i++)
                    link += "+" + search[i];
            }
            else return list;

            XmlDocument xml = RequestXml(link);

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

        private StreamReader Request(string link)
        {
            return Request(link, new Dictionary<string, string>());
        }

        private StreamReader Request(string link, Dictionary<string, string> data)
        {
            HttpWebResponse response;
            return Request(link, data, out response);
        }

        private StreamReader Request(string link, Dictionary<string, string> data, out HttpWebResponse response)
        {
            if (!IsConnected)
                throw new UserNotConnectedException();

            if (data.Any())
            {
                link += "?";
                List<string> dataInline = new List<string>();
                foreach (KeyValuePair<string, string> s in data)
                    dataInline.Add(string.Format("{0}={1}", s.Key, s.Value));
                link += string.Join("&", dataInline);
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

            StreamReader result = new StreamReader(response.GetResponseStream());

            response.Close();
            return result;
        }

        private XmlDocument RequestXml(string link)
        {
            return RequestXml(link, new Dictionary<string, string>());
        }

        private XmlDocument RequestXml(string link, Dictionary<string, string> data)
        {
            HttpWebResponse response;
            return RequestXml(link, data, out response);
        }

        private XmlDocument RequestXml(string link, Dictionary<string, string> data, out HttpWebResponse response)
        {
            XmlDocument result = new XmlDocument();
            StreamReader sr = Request(link, data, out response);
            result.LoadXml(HtmlDecodeAdvanced(sr.ReadToEnd()));
            sr.Close();
            return result;
        }

        private string HtmlDecodeAdvanced(string content)
        {
            StringBuilder output = new StringBuilder(content.Length);
            for (int i = 0; i < content.Length; i++)
            {
                if (content[i] == '&')
                {
                    int startOfEntity = i;
                    int endOfEntity = content.IndexOf(';', startOfEntity);
                    string entity = content.Substring(startOfEntity, endOfEntity - startOfEntity);
                    int unicodeNumber = (int)(HttpUtility.HtmlDecode(entity)[0]);
                    output.Append("&#" + unicodeNumber + ";");
                    i = endOfEntity;
                }
                else
                    output.Append(content[i]);
            }

            return output.ToString();
        }
    }
}