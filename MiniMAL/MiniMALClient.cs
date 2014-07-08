using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Xml;
using MiniMAL.Exceptions;
using MiniMAL.Internal;
using MiniMAL.Internal.Interfaces;

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
            string link = RequestLink.VerifyCredentials();
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
            return LoadUserList<AnimeList>();
        }

        static public AnimeList LoadAnimelist(string user)
        {
            return LoadUserList<AnimeList>(user);
        }

        public MangaList LoadMangalist()
        {
            return LoadUserList<MangaList>();
        }

        static public MangaList LoadMangalist(string user)
        {
            return LoadUserList<MangaList>(user);
        }

        public void AddAnime(int id, AnimeRequestData data)
        {
            AddEntry(id, data);
        }

        public void AddManga(int id, MangaRequestData data)
        {
            AddEntry(id, data);
        }

        public SearchResult<AnimeSearchEntry> SearchAnime(string[] search)
        {
            return Search<SearchResult<AnimeSearchEntry>>(search);
        }

        public SearchResult<MangaSearchEntry> SearchManga(string[] search)
        {
            return Search<SearchResult<MangaSearchEntry>>(search);
        }

        private TUserList LoadUserList<TUserList>() where TUserList : IUserList, new()
        {
            if (IsConnected)
                return LoadUserList<TUserList>(ClientData.Username);
            throw new UserNotConnectedException();
        }

        static private TUserList LoadUserList<TUserList>(string user)
            where TUserList : IUserList, new()
        {
            string link = RequestLink.UserList<TUserList>(user);
            XmlDocument xml = LoadXml(link);

            var list = new TUserList();
            list.LoadFromXml(xml);
            return list;
        }

        private void AddEntry<TRequestData>(int id, TRequestData data)
            where TRequestData : IRequestData, new()
        {
            string link = RequestLink.AddEntry<TRequestData>(id);
            var requestData = new Dictionary<string, string> {{"data", data.SerializeToString()}};

            HttpWebResponse response;
            Request(link, requestData, out response);
        }

        public TSearchResult Search<TSearchResult>(string[] search)
            where TSearchResult : ISearchResult, new()
        {
            string link = RequestLink.Search<TSearchResult>(search);
            XmlDocument xml = RequestXml(link);

            var result = new TSearchResult();
            result.LoadFromXml(xml);
            return result;
        }

        static private XmlDocument LoadXml(string link)
        {
            var xml = new XmlDocument();
            xml.Load(link);
            return xml;
        }

        private string Request(string link, Dictionary<string, string> data,
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
                baseStream.Close();
                throw;
            }

            Stream stream = response.GetResponseStream();
            StreamReader responseStream = null;
            if (stream != null)
                responseStream = new StreamReader(stream);

            if (responseStream == null)
                return "";

            string result = responseStream.ReadToEnd();

            responseStream.Close();
            stream.Close();
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
            string xml = Request(link, data, out response);
            result.LoadXml(HtmlDecodeAdvanced(xml));
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