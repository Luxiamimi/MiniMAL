namespace MiniMAL
{
    static public class WebsiteLink
    {
        public const string Home = "http://myanimelist.net/";

        public const string MyPanel = "http://myanimelist.net/panel.php";
        public const string MyMessages = "http://myanimelist.net/mymessages.php";
        public const string MyFriends = "http://myanimelist.net/myfriends.php";
        public const string MyClubs = "http://myanimelist.net/clubs.php?action=myclubs";
        public const string MyBlog = "http://myanimelist.net/myblog.php";
        public const string MyReviews = "http://myanimelist.net/myreviews.php";
        public const string MyRecommendations = "http://myanimelist.net/myrecommendations.php";

        public const string AnimePortal = "http://myanimelist.net/anime.php";
        public const string AnimeReviews = "http://myanimelist.net/reviews.php?t=anime";
        public const string AnimeRecommendations = "http://myanimelist.net/recommendations.php?t=anime";
        public const string TopAnime = "http://myanimelist.net/topanime.php";
        public const string FansubGroups = "http://myanimelist.net/fansub-groups.php";

        public const string MangaPortal = "http://myanimelist.net/manga.php";
        public const string MangaReviews = "http://myanimelist.net/reviews.php?t=manga";
        public const string MangaRecommendations = "http://myanimelist.net/recommendations.php?t=manga";
        public const string TopManga = "http://myanimelist.net/topmanga.php";

        public const string ForumsPortal = "http://myanimelist.net/forum/";
        public const string ClubsPortal = "http://myanimelist.net/clubs.php";
        public const string BlogsPortal = "http://myanimelist.net/blog.php";
        public const string NewsPortal = "http://myanimelist.net/news.php";
        public const string TopFavorites = "http://myanimelist.net/favorites.php";

        private const string ProfileFormat = "http://myanimelist.net/profile/{0}";
        private const string AnimelistFormat = "http://myanimelist.net/animelist/{0}";
        private const string MangalistFormat = "http://myanimelist.net/mangalist/{0}";
        private const string BlogFormat = "http://myanimelist.net/blog/{0}";

        private const string AnimeFormat = "http://myanimelist.net/anime/{0}";
        private const string MangaFormat = "http://myanimelist.net/manga/{0}";

        private const string TopicFormat = "http://myanimelist.net/forum/?topicid={0}";
        private const string ClubFormat = "http://myanimelist.net/clubs.php?cid={0}";

        private const string PeopleFormat = "http://myanimelist.net/people/{0}";
        private const string CharacterFormat = "http://myanimelist.net/character/{0}";

        static public string Profile(string username)
        {
            return string.Format(ProfileFormat, username);
        }

        static public string Animelist(string username)
        {
            return string.Format(AnimelistFormat, username);
        }

        static public string Mangalist(string username)
        {
            return string.Format(MangalistFormat, username);
        }

        static public string Blog(string username)
        {
            return string.Format(BlogFormat, username);
        }

        static public string Anime(int id)
        {
            return string.Format(AnimeFormat, id);
        }

        static public string Manga(int id)
        {
            return string.Format(MangaFormat, id);
        }

        static public string Topic(int id)
        {
            return string.Format(TopicFormat, id);
        }

        static public string Club(int id)
        {
            return string.Format(ClubFormat, id);
        }

        static public string People(int id)
        {
            return string.Format(PeopleFormat, id);
        }

        static public string Character(int id)
        {
            return string.Format(CharacterFormat, id);
        }
    }
}