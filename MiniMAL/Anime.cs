using System;
using System.Xml;

namespace MiniMAL
{
    public class Anime
    {
		public enum TypeAnime
        {
			None = 0, TV = 1, OVA = 2, Movie = 3, Special = 4, ONA = 5
        }

        public enum AiringStatus
        {
			None = 0, Airing = 1, Finished = 2, NoYetAiring = 3
        }

        public enum WatchingStatus
        {
            None = 0, Watching = 1, Completed = 2, OnHold = 3, Dropped = 4, PlanToWatch = 6
        }

        public int ID { get; set; }
        public string Title { get; set; }
        public string[] Synonyms { get; set; }
		public TypeAnime Type { get; set; }
        public int Episodes { get; set; }
        public AiringStatus Status { get; set; }
        public DateTime AiringStart { get; set; }
        public DateTime AiringEnd { get; set; }
        public string ImageUrl { get; set; }
        public int MyID { get; set; }
        public int MyWatchedEp { get; set; }
        public DateTime MyStartDate { get; set; }
        public DateTime MyFinishDate { get; set; }
        public int MyScore { get; set; }
        public WatchingStatus MyWatchingStatus { get; set; }
        public int MyRewatching { get; set; }
        public int MyRewatchingEpisodes { get; set; }
		public string MyTags { get; set; }

		public string TitleForUrl
		{
			get
			{
				string result = Title.Replace(" ", "_").Replace("?", "").Replace(",", "").Replace("\x27", "");
				return String.Join("_", result.Split(new char[] { '_' }
					, StringSplitOptions.RemoveEmptyEntries));
			}
		}

		public static Anime LoadFromXmlNode(XmlNode e)
		{
            Anime a = new Anime();
			a.ID = e["series_animedb_id"].InnerText != "" ? Int32.Parse(e["series_animedb_id"].InnerText) : 0;
            a.Title = e["series_title"].InnerText;
            a.Synonyms = e["series_synonyms"].InnerText.Split(new string[] { "; " }, StringSplitOptions.RemoveEmptyEntries);
            a.Type = e["series_type"].InnerText != "" ? (TypeAnime)Int32.Parse(e["series_type"].InnerText) : TypeAnime.None;
            a.Episodes = e["series_episodes"].InnerText != "" ? Int32.Parse(e["series_episodes"].InnerText) : 0;
            a.Status = e["series_status"].InnerText != "" ? (AiringStatus)Int32.Parse(e["series_status"].InnerText) : AiringStatus.None;
            a.AiringStart = MiniMALTools.StringToDate(e["series_start"].InnerText);
            a.AiringEnd = MiniMALTools.StringToDate(e["series_end"].InnerText);
            a.ImageUrl = e["series_image"].InnerText;
            a.MyID = e["my_id"].InnerText != "" ? Int32.Parse(e["my_id"].InnerText) : 0;
            a.MyWatchedEp = e["my_watched_episodes"].InnerText != "" ? Int32.Parse(e["my_watched_episodes"].InnerText) : 0;
            a.MyStartDate = MiniMALTools.StringToDate(e["my_start_date"].InnerText);
            a.MyFinishDate = MiniMALTools.StringToDate(e["my_finish_date"].InnerText);
            a.MyScore = e["my_score"].InnerText != "" ? Int32.Parse(e["my_score"].InnerText) : 0;
            a.MyWatchingStatus = e["my_status"].InnerText != "" ? (WatchingStatus)Int32.Parse(e["my_status"].InnerText) : WatchingStatus.None;
            a.MyRewatching = e["my_rewatching"].InnerText != "" ? Int32.Parse(e["my_rewatching_ep"].InnerText) : 0;
            a.MyRewatchingEpisodes = e["my_rewatching_ep"].InnerText != "" ? Int32.Parse(e["my_rewatching_ep"].InnerText) : 0;
            a.MyTags = e["my_tags"].InnerText;
            return a;
		}

        public override string ToString()
        {
            return Title;
        }
    }
}
