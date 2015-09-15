using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Xml;
using MiniMAL.Anime;
using MiniMAL.Exceptions;
using MiniMAL.Generic;
using MiniMAL.Manga;

namespace MiniMAL
{
    public class MiniMALClient
    {
        private const string ConfigFilename = "configMiniMAL.xml";
        public bool IsConnected { get; private set; }
        public ClientData ClientData { get; private set; }

        public MiniMALClient()
        {
            IsConnected = false;
        }

        #region Public

        public void SaveConfig()
        {
            ClientData.Save(ClientData, ConfigFilename);
        }

        public void LoadConfig()
        {
            try
            {
                ClientData = ClientData.Load(ConfigFilename);
            }
            catch (FileNotFoundException)
            {
                throw new ConfigFileNotFoundException();
            }
            catch (InvalidOperationException)
            {
                throw new ConfigFileCorruptException();
            }

            Login(ClientData.Username, ClientData.DecryptedPassword);
        }

        public void Login(string username, string password)
        {
            const string link = RequestLink.VerifyCredentials;
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

            ClientData = new ClientData(username, password);
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

        public AddRequestResult AddAnime(int id, AnimeRequestData data)
        {
            return AddAnimeAsync(id, data, CancellationToken.None).Result;
        }

        public AddRequestResult AddManga(int id, MangaRequestData data)
        {
            return AddMangaAsync(id, data, CancellationToken.None).Result;
        }

        public UpdateRequestResult UpdateAnime(int id, AnimeRequestData data)
        {
            return UpdateAnimeAsync(id, data, CancellationToken.None).Result;
        }

        public UpdateRequestResult UpdateManga(int id, MangaRequestData data)
        {
            return UpdateMangaAsync(id, data, CancellationToken.None).Result;
        }

        public DeleteRequestResult DeleteAnime(int id)
        {
            return DeleteAnimeAsync(id, CancellationToken.None).Result;
        }

        public DeleteRequestResult DeleteManga(int id)
        {
            return DeleteMangaAsync(id, CancellationToken.None).Result;
        }

        public List<AnimeSearchEntry> SearchAnime(string search)
        {
            return SearchAnimeAsync(search, CancellationToken.None).Result;
        }

        public List<MangaSearchEntry> SearchManga(string search)
        {
            return SearchMangaAsync(search, CancellationToken.None).Result;
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

        public async Task<AddRequestResult> AddAnimeAsync(int id, AnimeRequestData data, CancellationToken ct)
        {
            return await AddEntryAsync<AnimeRequestData, AnimeRequestSerializable>(id, data, ct);
        }

        public async Task<AddRequestResult> AddMangaAsync(int id, MangaRequestData data, CancellationToken ct)
        {
            return await AddEntryAsync<MangaRequestData, MangaRequestSerializable>(id, data, ct);
        }

        public async Task<UpdateRequestResult> UpdateAnimeAsync(int id, AnimeRequestData data, CancellationToken ct)
        {
            return await UpdateEntryAsync<AnimeRequestData, AnimeRequestSerializable>(id, data, ct);
        }

        public async Task<UpdateRequestResult> UpdateMangaAsync(int id, MangaRequestData data, CancellationToken ct)
        {
            return await UpdateEntryAsync<MangaRequestData, MangaRequestSerializable>(id, data, ct);
        }

        public async Task<DeleteRequestResult> DeleteAnimeAsync(int id, CancellationToken ct)
        {
            return await DeleteEntryAsync<AnimeRequestData>(id, ct);
        }

        public async Task<DeleteRequestResult> DeleteMangaAsync(int id, CancellationToken ct)
        {
            return await DeleteEntryAsync<MangaRequestData>(id, ct);
        }

        public async Task<List<AnimeSearchEntry>> SearchAnimeAsync(string search, CancellationToken cancellationToken)
        {
            return await SearchAsync<SearchResult<AnimeSearchEntry, AnimeType, AiringStatus>>(search, cancellationToken);
        }

        public async Task<List<MangaSearchEntry>> SearchMangaAsync(string search, CancellationToken cancellationToken)
        {
            return await SearchAsync<SearchResult<MangaSearchEntry, MangaType, PublishingStatus>>(search, cancellationToken);
        }

        #endregion Public

        #region Generic

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

        private async Task<AddRequestResult> AddEntryAsync<TRequestData, TRequestSerializable>(int id, TRequestData data, CancellationToken ct)
            where TRequestData : IRequestData, new()
            where TRequestSerializable : IRequestSerializable<TRequestData>, new()
        {
            var serialize = new TRequestSerializable();
            serialize.GetData(data);

            string link = RequestLink.AddEntry<TRequestData>(id);
            var requestData = new Dictionary<string, string>
            {
                {"data", serialize.SerializeDataToString()}
            };

            try
            {
                await RequestAsync(link, requestData, ct);
            }
            catch (RequestException e)
            {
                if (e.Message.IndexOf("already", StringComparison.OrdinalIgnoreCase) >= 0)
                    return AddRequestResult.AlreadyInTheList;

                throw;
            }

            return AddRequestResult.Created;
        }

        private async Task<UpdateRequestResult> UpdateEntryAsync<TRequestData, TRequestSerializable>(int id, TRequestData data, CancellationToken ct)
            where TRequestData : IRequestData, new()
            where TRequestSerializable : IRequestSerializable<TRequestData>, new()
        {
            var serialize = new TRequestSerializable();
            serialize.GetData(data);

            string link = RequestLink.UpdateEntry<TRequestData>(id);
            var requestData = new Dictionary<string, string>
            {
                {"data", serialize.SerializeDataToString()}
            };

            try
            {
                await RequestAsync(link, requestData, ct);
            }
            catch (RequestException e)
            {
                if (e.Message.Contains("No parameters passed in"))
                    return UpdateRequestResult.NoParametersPassed;

                throw;
            }

            return UpdateRequestResult.Updated;
        }

        private async Task<DeleteRequestResult> DeleteEntryAsync<TRequestData>(int id, CancellationToken ct)
            where TRequestData : IRequestData, new()
        {
            string link = RequestLink.DeleteEntry<TRequestData>(id);

            await RequestAsync(link, ct);

            return DeleteRequestResult.Deleted;
        }

        private async Task<TSearchResult> SearchAsync<TSearchResult>(string search, CancellationToken ct)
            where TSearchResult : ISearchResult, new()
        {
            string link = RequestLink.Search<TSearchResult>(search);
            XmlDocument xml = await RequestXmlAsync(link, ct);

            var result = new TSearchResult();
            result.LoadFromXml(xml);
            return result;
        }

        #endregion Generic

        #region Request

        private async Task<string> RequestAsync(string link, CancellationToken ct)
        {
            return await RequestAsync(link, new Dictionary<string, string>(), ct);
        }

        private async Task<string> RequestAsync(string link, Dictionary<string, string> data, CancellationToken ct)
        {
            ct.ThrowIfCancellationRequested();

            if (!IsConnected)
                throw new UserNotConnectedException();

            if (data.Any())
            {
                link += "?";
                var dataInline = new List<string>();
                foreach (KeyValuePair<string, string> pair in data)
                    dataInline.Add(string.Format("{0}={1}", pair.Key, pair.Value));
                link += string.Join("&", dataInline);
            }

            var request = (HttpWebRequest)WebRequest.Create(link);
            request.Credentials = new NetworkCredential(ClientData.Username, ClientData.DecryptedPassword);
            request.PreAuthenticate = true;
            request.Timeout = 10 * 1000;

            HttpWebResponse response;
            using (ct.Register(() => request.Abort(), false))
            {
                try
                {
                    response = (HttpWebResponse)(await request.GetResponseAsync());
                    ct.ThrowIfCancellationRequested();
                }
                catch (WebException e)
                {
                    if (ct.IsCancellationRequested)
                        throw new OperationCanceledException(e.Message, e, ct);

                    var error = (HttpWebResponse)e.Response;
                    if (error == null)
                        throw;

                    using (Stream baseStream = error.GetResponseStream())
                    {
                        if (baseStream == null)
                            throw;

                        using (var readStream = new StreamReader(baseStream, Encoding.UTF8))
                        {
                            string errorMessage = readStream.ReadToEnd();
                            throw new RequestException(errorMessage);
                        }
                    }
                }
            }

            string result;
            using (Stream stream = response.GetResponseStream())
            {
                if (stream == null)
                    return "";

                using (var responseStream = new StreamReader(stream))
                {
                    result = await responseStream.ReadToEndAsync();
                }
            }

            return result;
        }

        static private async Task<XmlDocument> LoadXmlAsync(string link)
        {
            var client = new WebClient();
            var xml = new XmlDocument();
            string content = await client.DownloadStringTaskAsync(link);
            xml.LoadXml(HtmlDecodeAdvanced(content));
            return xml;
        }

        private async Task<XmlDocument> RequestXmlAsync(string link, CancellationToken ct)
        {
            return await RequestXmlAsync(link, new Dictionary<string, string>(), ct);
        }

        private async Task<XmlDocument> RequestXmlAsync(string link, Dictionary<string, string> data, CancellationToken ct)
        {
            var result = new XmlDocument();
            string xml = await RequestAsync(link, data, ct);
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

        #endregion Request

        #region Enum

        public enum AddRequestResult
        {
            Created,
            AlreadyInTheList
        }

        public enum DeleteRequestResult
        {
            Deleted
        }

        public enum UpdateRequestResult
        {
            Updated,
            NoParametersPassed
        }

        #endregion Enum
    }
}