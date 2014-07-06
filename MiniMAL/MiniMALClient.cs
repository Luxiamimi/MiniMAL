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
        private const string ConfigFilename = "configMiniMAL.xml";

        public MiniMALClient()
        {
            IsConnected = false;
        }

        public void SaveConfig()
        {
            MiniMALClientData.Save(ClientData, ConfigFilename);
        }

        public void LoadConfig()
        {
            try
            {
                ClientData = MiniMALClientData.Load(ConfigFilename);
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
            const string link = "http://myanimelist.net/api/account/verify_credentials.xml";
            WebRequest request = WebRequest.Create(link);
            request.Credentials = new NetworkCredential(username, password);

            try
            {
                request.GetResponse();
            }
            catch (WebException e)
            {
                IsConnected = false;
                var error = (HttpWebResponse)e.Response;

                if (error.StatusCode == HttpStatusCode.Unauthorized)
                    throw new UserUnauthorizedException();
                throw;
            }

            ClientData = new MiniMALClientData(username, password);
            IsConnected = true;
        }

        public AnimeList LoadAnimelist()
        {
            if (IsConnected)
                return LoadAnimelist(ClientData.Username);
            throw new UserNotConnectedException();
        }

        static public AnimeList LoadAnimelist(string user)
        {
            var list = new AnimeList();

            string link =
                string.Format("http://myanimelist.net/malappinfo.php?u={0}&type=anime&status=all",
                    user);
            XmlDocument xml = LoadXml(link);

            if (xml.DocumentElement == null)
                return list;
            foreach (XmlNode e in xml.DocumentElement.ChildNodes)
            {
                if (e.Name != "anime")
                    continue;
                var a = new Anime();
                a.LoadFromXmlNode(e);
                list.Add(a);
            }
            return list;
        }

        public void AddAnime(int id, AnimeRequestData data)
        {
            string link = string.Format("http://myanimelist.net/api/animelist/add/{0}.xml", id);

            var requestData = new Dictionary<string, string> {{"data", data.SerializeToString()}};

            HttpWebResponse response;
            Request(link, requestData, out response);
        }

        public MangaList LoadMangalist()
        {
            if (IsConnected)
                return LoadMangalist(ClientData.Username);
            throw new UserNotConnectedException();
        }

        static public MangaList LoadMangalist(string user)
        {
            if (user == null)
                throw new ArgumentNullException("user");
            var list = new MangaList();

            string link =
                string.Format("http://myanimelist.net/malappinfo.php?u={0}&type=manga&status=all",
                    user);
            XmlDocument xml = LoadXml(link);

            if (xml.DocumentElement == null)
                return list;
            foreach (XmlNode e in xml.DocumentElement.ChildNodes)
            {
                if (e.Name != "manga")
                    continue;

                var m = new Manga();
                m.LoadFromXmlNode(e);
                list.Add(m);
            }
            return list;
        }

        public void AddManga(int id, MangaRequestData data)
        {
            string link = string.Format("http://myanimelist.net/api/mangalist/add/{0}.xml", id);

            var requestData = new Dictionary<string, string> {{"data", data.SerializeToString()}};

            HttpWebResponse response;
            Request(link, requestData, out response);
        }

        public List<AnimeSearchEntry> SearchAnime(string[] search)
        {
            var list = new List<AnimeSearchEntry>();

            string link = "http://myanimelist.net/api/anime/search.xml?q=";

            if (search.Any())
            {
                link += search[0];
                for (int i = 1; i < search.Length; i++)
                    link += "+" + search[i];
            }
            else
                return list;

            XmlDocument xml = RequestXml(link);

            if (xml.DocumentElement == null)
                return list;
            foreach (XmlNode e in xml.DocumentElement.ChildNodes)
            {
                if (e.Name != "entry")
                    continue;

                var a = new AnimeSearchEntry();
                a.LoadFromXmlNode(e);
                list.Add(a);
            }

            return list;
        }

        public List<MangaSearchEntry> SearchManga(string[] search)
        {
            var list = new List<MangaSearchEntry>();

            string link = "http://myanimelist.net/api/manga/search.xml?q=";

            if (search.Any())
            {
                link += search[0];
                for (int i = 1; i < search.Length; i++)
                    link += "+" + search[i];
            }
            else
                return list;

            XmlDocument xml = RequestXml(link);

            if (xml.DocumentElement == null)
                return list;
            foreach (XmlNode e in xml.DocumentElement.ChildNodes)
            {
                if (e.Name != "entry")
                    continue;

                var m = new MangaSearchEntry();
                m.LoadFromXmlNode(e);
                list.Add(m);
            }

            return list;
        }

        static private XmlDocument LoadXml(string link)
        {
            var xml = new XmlDocument();
            xml.Load(link);
            return xml;
        }

        private StreamReader Request(string link, Dictionary<string, string> data,
                                     out HttpWebResponse response)
        {
            if (!IsConnected)
                throw new UserNotConnectedException();

            if (data.Any())
            {
                link += "?";
                var dataInline = new List<string>();
                foreach (var s in data)
                    dataInline.Add(string.Format("{0}={1}", s.Key, s.Value));
                link += string.Join("&", dataInline);
            }

            var request = (HttpWebRequest)WebRequest.Create(link);
            request.Credentials = new NetworkCredential(ClientData.Username,
                ClientData.DecryptedPassword);
            request.PreAuthenticate = true;
            request.Timeout = 10 * 1000;

            try
            {
                response = (HttpWebResponse)request.GetResponse();
            }
            catch (WebException e)
            {
                var error = (HttpWebResponse)e.Response;
                if (error == null)
                    throw;

                Stream baseStream = error.GetResponseStream();
                if (baseStream == null)
                    throw;

                var readStream = new StreamReader(baseStream, Encoding.UTF8);
                Console.WriteLine(readStream.ReadToEnd());

                readStream.Close();
                throw;
            }

            Stream responseStream = response.GetResponseStream();
            StreamReader result = null;
            if (responseStream != null)
                result = new StreamReader(responseStream);

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

        private XmlDocument RequestXml(string link, Dictionary<string, string> data,
                                       out HttpWebResponse response)
        {
            var result = new XmlDocument();
            StreamReader sr = Request(link, data, out response);
            result.LoadXml(HtmlDecodeAdvanced(sr.ReadToEnd()));
            sr.Close();
            return result;
        }

        static private string HtmlDecodeAdvanced(string content)
        {
            var output = new StringBuilder(content.Length);
            for (int i = 0; i < content.Length; i++)
                if (content[i] == '&')
                {
                    int startOfEntity = i;
                    int endOfEntity = content.IndexOf(';', startOfEntity);
                    string entity = content.Substring(startOfEntity, endOfEntity - startOfEntity);
                    int unicodeNumber = HttpUtility.HtmlDecode(entity)[0];
                    output.Append("&#" + unicodeNumber + ";");
                    i = endOfEntity;
                }
                else
                    output.Append(content[i]);

            return output.ToString();
        }
    }
}