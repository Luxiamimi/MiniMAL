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
            ID = MiniMALConverter.XmlToInt(e["series_animedb_id"]);
            Title = e["series_title"].InnerText;
            Synonyms = e["series_synonyms"].InnerText.Split(new string[] { "; " }, StringSplitOptions.RemoveEmptyEntries);
            Type = e["series_type"].InnerText != "" ? (TypeAnime)Int32.Parse(e["series_type"].InnerText) : TypeAnime.None;
            Episodes = MiniMALConverter.XmlToInt(e["series_episodes"]);
            Status = e["series_status"].InnerText != "" ? (AiringStatus)Int32.Parse(e["series_status"].InnerText) : AiringStatus.None;
            StartDate = MiniMALConverter.XmlToDate(e["series_start"]);
            EndDate = MiniMALConverter.XmlToDate(e["series_end"]);
            ImageUrl = e["series_image"].InnerText;
            MyID = MiniMALConverter.XmlToInt(e["my_id"]);
            MyWatchedEp = MiniMALConverter.XmlToInt(e["my_watched_episodes"]);
            MyStartDate = MiniMALConverter.XmlToDate(e["my_start_date"]);
            MyEndDate = MiniMALConverter.XmlToDate(e["my_finish_date"]);
            MyScore = MiniMALConverter.XmlToInt(e["my_score"]);
            MyStatus = e["my_status"].InnerText != "" ? (WatchingStatus)Int32.Parse(e["my_status"].InnerText) : WatchingStatus.None;
            MyRewatchingCount = MiniMALConverter.XmlToInt(e["my_rewatching"]);
            MyRewatchingEpisodes = MiniMALConverter.XmlToInt(e["my_rewatching_ep"]);
            MyTags = e["my_tags"].InnerText;
		}
    }
}
