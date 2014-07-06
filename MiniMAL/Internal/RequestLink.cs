namespace MiniMAL.Internal
{
    static internal class RequestLink
    {
        private const string VerifyCredentialsFormat = "http://myanimelist.net/api/account/verify_credentials.xml";

        private const string AnimelistFormat = "http://myanimelist.net/malappinfo.php?u={0}&type=anime&status=all";
        private const string AddAnimeFormat = "http://myanimelist.net/api/animelist/add/{0}.xml";

        private const string MangalistFormat = "http://myanimelist.net/malappinfo.php?u={0}&type=manga&status=all";
        private const string AddMangaFormat = "http://myanimelist.net/api/mangalist/add/{0}.xml";

        private const string SearchAnimeFormat = "http://myanimelist.net/api/anime/search.xml?q={0}";
        private const string SearchMangaFormat = "http://myanimelist.net/api/manga/search.xml?q={0}";

        static public string VerifyCredentials()
        {
            return VerifyCredentialsFormat;
        }

        static public string Animelist(string user)
        {
            return string.Format(AnimelistFormat, user);
        }

        static public string AddAnime(int id)
        {
            return string.Format(AddAnimeFormat, id);
        }

        static public string Mangalist(string user)
        {
            return string.Format(MangalistFormat, user);
        }

        static public string AddManga(int id)
        {
            return string.Format(AddMangaFormat, id);
        }

        static public string SearchAnime(string[] search)
        {
            return string.Format(SearchAnimeFormat, string.Join("+", search));
        }

        static public string SearchManga(string[] search)
        {
            return string.Format(SearchMangaFormat, string.Join("+", search));
        }
    }
}
