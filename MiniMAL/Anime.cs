using System;
using System.Xml;

namespace MiniMAL
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

    public class Anime : Entry<TypeAnime, AiringStatus, WatchingStatus>
    {
        public int Episodes { get; set; }
        public int MyWatchedEp { get; set; }
        public int MyRewatchingCount { get; set; }
        public int MyRewatchingEpisodes { get; set; }

		public override void LoadFromXmlNode(XmlNode e)
		{
			ID = e["series_animedb_id"].InnerText != "" ? Int32.Parse(e["series_animedb_id"].InnerText) : 0;
            Title = e["series_title"].InnerText;
            Synonyms = e["series_synonyms"].InnerText.Split(new string[] { "; " }, StringSplitOptions.RemoveEmptyEntries);
            Type = e["series_type"].InnerText != "" ? (TypeAnime)Int32.Parse(e["series_type"].InnerText) : TypeAnime.None;
            Episodes = e["series_episodes"].InnerText != "" ? Int32.Parse(e["series_episodes"].InnerText) : 0;
            Status = e["series_status"].InnerText != "" ? (AiringStatus)Int32.Parse(e["series_status"].InnerText) : AiringStatus.None;
            StartDate = MiniMALTools.StringToDate(e["series_start"].InnerText);
            EndDate = MiniMALTools.StringToDate(e["series_end"].InnerText);
            ImageUrl = e["series_image"].InnerText;
            MyID = e["my_id"].InnerText != "" ? Int32.Parse(e["my_id"].InnerText) : 0;
            MyWatchedEp = e["my_watched_episodes"].InnerText != "" ? Int32.Parse(e["my_watched_episodes"].InnerText) : 0;
            MyStartDate = MiniMALTools.StringToDate(e["my_start_date"].InnerText);
            MyEndDate = MiniMALTools.StringToDate(e["my_finish_date"].InnerText);
            MyScore = e["my_score"].InnerText != "" ? Int32.Parse(e["my_score"].InnerText) : 0;
            MyStatus = e["my_status"].InnerText != "" ? (WatchingStatus)Int32.Parse(e["my_status"].InnerText) : WatchingStatus.None;
            MyRewatchingCount = e["my_rewatching"].InnerText != "" ? Int32.Parse(e["my_rewatching_ep"].InnerText) : 0;
            MyRewatchingEpisodes = e["my_rewatching_ep"].InnerText != "" ? Int32.Parse(e["my_rewatching_ep"].InnerText) : 0;
            MyTags = e["my_tags"].InnerText;
		}
    }
}
