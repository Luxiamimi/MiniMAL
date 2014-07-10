using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
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
            return LoadAnimelistAsync().Result;
        }

        static public AnimeList LoadAnimelist(string user)
        {
            return LoadAnimelistAsync(user).Result;
        }

        public MangaList LoadMangalist()
        {
            return LoadMangalistAsync().Result;
        }

        static public MangaList LoadMangalist(string user)
        {
            return LoadMangalistAsync(user).Result;
        }

        public ListRequestResult AddAnime(int id, AnimeRequestData data)
        {
            return AddAnimeAsync(id, data).Result;
        }

        public ListRequestResult AddManga(int id, MangaRequestData data)
        {
            return AddMangaAsync(id, data).Result;
        }

        public SearchResult<AnimeSearchEntry> SearchAnime(string[] search)
        {
            return SearchAnimeAsync(search).Result;
        }

        public SearchResult<MangaSearchEntry> SearchManga(string[] search)
        {
            return SearchMangaAsync(search).Result;
        }

        public async Task<AnimeList> LoadAnimelistAsync()
        {
            return await LoadUserListAsync<AnimeList>();
        }

        static public async Task<AnimeList> LoadAnimelistAsync(string user)
        {
            return await LoadUserListAsync<AnimeList>(user);
        }

        public async Task<MangaList> LoadMangalistAsync()
        {
            return await LoadUserListAsync<MangaList>();
        }

        static public async Task<MangaList> LoadMangalistAsync(string user)
        {
            return await LoadUserListAsync<MangaList>(user);
        }

        public async Task<ListRequestResult> AddAnimeAsync(int id, AnimeRequestData data)
        {
            return await AddEntryAsync(id, data);
        }

        public async Task<ListRequestResult> AddMangaAsync(int id, MangaRequestData data)
        {
            return await AddEntryAsync(id, data);
        }

        public async Task<SearchResult<AnimeSearchEntry>> SearchAnimeAsync(string[] search)
        {
            return await SearchAsync<SearchResult<AnimeSearchEntry>>(search);
        }

        public async Task<SearchResult<MangaSearchEntry>> SearchMangaAsync(string[] search)
        {
            return await SearchAsync<SearchResult<MangaSearchEntry>>(search);
        }

        private Task<TUserList> LoadUserListAsync<TUserList>() where TUserList : IUserList, new()
        {
            if (IsConnected)
                return LoadUserListAsync<TUserList>(ClientData.Username);
            throw new UserNotConnectedException();
        }

        static private async Task<TUserList> LoadUserListAsync<TUserList>(string user)
            where TUserList : IUserList, new()
        {
            string link = RequestLink.UserList<TUserList>(user);
            XmlDocument xml = await LoadXmlAsync(link);

            var list = new TUserList();
            list.LoadFromXml(xml);
            return list;
        }

        private async Task<ListRequestResult> AddEntryAsync<TRequestData>(int id, TRequestData data)
            where TRequestData : IRequestData, new()
        {
            string link = RequestLink.AddEntry<TRequestData>(id);
            var requestData = new Dictionary<string, string> {{"data", data.SerializeToString()}};

            string result = await RequestAsync(link, requestData);

            if (result.Contains("Created"))
                return ListRequestResult.Created;
            if (result.IndexOf("already", StringComparison.OrdinalIgnoreCase) >= 0)
                return ListRequestResult.AlreadyInTheList;
            return ListRequestResult.None;
        }

        private async Task<TSearchResult> SearchAsync<TSearchResult>(string[] search)
            where TSearchResult : ISearchResult, new()
        {
            string link = RequestLink.Search<TSearchResult>(search);
            XmlDocument xml = await RequestXmlAsync(link);

            var result = new TSearchResult();
            result.LoadFromXml(xml);
            return result;
        }

        static private async Task<XmlDocument> LoadXmlAsync(string link)
        {
            var client = new WebClient();
            var xml = new XmlDocument();
            xml.LoadXml(HtmlDecodeAdvanced(await client.DownloadStringTaskAsync(link)));
            return xml;
        }

        private async Task<string> RequestAsync(string link, Dictionary<string, string> data)
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

            HttpWebResponse response;
            try
            {
                response = (HttpWebResponse)(await request.GetResponseAsync());
            }
            catch (WebException e)
            {
                var error = (HttpWebResponse)e.Response;
                if (error == null)
                    throw;

                using (Stream baseStream = error.GetResponseStream())
                {
                    if (baseStream == null)
                        throw;

                    using (var readStream = new StreamReader(baseStream, Encoding.UTF8))
                        return readStream.ReadToEnd();
                }
            }

            string result;
            using (Stream stream = response.GetResponseStream())
            {
                if (stream == null)
                    return "";

                using (var responseStream = new StreamReader(stream))
                    result = await responseStream.ReadToEndAsync();
            }

            return result;
        }

        private async Task<XmlDocument> RequestXmlAsync(string link)
        {
            return await RequestXmlAsync(link, new Dictionary<string, string>());
        }

        private async Task<XmlDocument> RequestXmlAsync(string link, Dictionary<string, string> data)
        {
            var result = new XmlDocument();
            string xml = await RequestAsync(link, data);
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