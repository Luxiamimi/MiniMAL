using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using MiniMAL.Exceptions;
using MiniMAL.Internal;

namespace MiniMAL
{
    // TODO : update/edit a list
    public partial class MiniMALClient
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
    }
}