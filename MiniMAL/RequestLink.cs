using System;
using MiniMAL.Anime;
using MiniMAL.Generic;
using MiniMAL.Manga;

namespace MiniMAL
{
    static internal class RequestLink
    {
        public const string VerifyCredentials = "http://myanimelist.net/api/account/verify_credentials.xml";

        private const string AnimelistFormat = "http://myanimelist.net/malappinfo.php?u={0}&type=anime&status=all";
        private const string MangalistFormat = "http://myanimelist.net/malappinfo.php?u={0}&type=manga&status=all";

        private const string AddAnimeFormat = "http://myanimelist.net/api/animelist/add/{0}.xml";
        private const string AddMangaFormat = "http://myanimelist.net/api/mangalist/add/{0}.xml";

        private const string UpdateAnimeFormat = "http://myanimelist.net/api/animelist/update/{0}.xml";
        private const string UpdateMangaFormat = "http://myanimelist.net/api/mangalist/update/{0}.xml";

        private const string DeleteAnimeFormat = "http://myanimelist.net/api/animelist/delete/{0}.xml";
        private const string DeleteMangaFormat = "http://myanimelist.net/api/mangalist/delete/{0}.xml";

        private const string SearchAnimeFormat = "http://myanimelist.net/api/anime/search.xml?q={0}";
        private const string SearchMangaFormat = "http://myanimelist.net/api/manga/search.xml?q={0}";

        static public string UserList<TUserList>(string user) where TUserList : IUserList, new()
        {
            string format;
            Type type = typeof(TUserList);

            if (type == typeof(AnimeList))
                format = AnimelistFormat;
            else if (type == typeof(MangaList))
                format = MangalistFormat;
            else
                throw new ArgumentException();

            return string.Format(format, user);
        }

        static public string AddEntry<TRequestData>(int id) where TRequestData : IRequestData, new()
        {
            string format;
            Type type = typeof(TRequestData);

            if (type == typeof(AnimeRequestData))
                format = AddAnimeFormat;
            else if (type == typeof(MangaRequestData))
                format = AddMangaFormat;
            else
                throw new ArgumentException();

            return string.Format(format, id);
        }

        static public string UpdateEntry<TRequestData>(int id) where TRequestData : IRequestData, new()
        {
            string format;
            Type type = typeof(TRequestData);

            if (type == typeof(AnimeRequestData))
                format = UpdateAnimeFormat;
            else if (type == typeof(MangaRequestData))
                format = UpdateMangaFormat;
            else
                throw new ArgumentException();

            return string.Format(format, id);
        }

        static public string DeleteEntry<TRequestData>(int id) where TRequestData : IRequestData, new()
        {
            string format;
            Type type = typeof(TRequestData);

            if (type == typeof(AnimeRequestData))
                format = DeleteAnimeFormat;
            else if (type == typeof(MangaRequestData))
                format = DeleteMangaFormat;
            else
                throw new ArgumentException();

            return string.Format(format, id);
        }

        static public string Search<TSearchResult>(string[] search) where TSearchResult : ISearchResult, new()
        {
            string format;
            Type type = typeof(TSearchResult);

            if (type == typeof(SearchResult<AnimeSearchEntry, AnimeType, AiringStatus>))
                format = SearchAnimeFormat;
            else if (type == typeof(SearchResult<MangaSearchEntry, MangaType, PublishingStatus>))
                format = SearchMangaFormat;
            else
                throw new ArgumentException();

            return string.Format(format, string.Join("+", search));
        }
    }
}