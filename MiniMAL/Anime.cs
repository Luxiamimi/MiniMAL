using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
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

		public void LoadFromXmlNode(XmlNode e)
		{
			ID = e["series_animedb_id"].InnerText != "" ? Int32.Parse(e["series_animedb_id"].InnerText) : 0;
			Title = e["series_title"].InnerText;
			Synonyms = e["series_synonyms"].InnerText.Split(new string[] {"; "}, StringSplitOptions.RemoveEmptyEntries);
			Type = e ["series_type"].InnerText != "" ? (TypeAnime)Int32.Parse(e ["series_type"].InnerText) : TypeAnime.None;
			Episodes = e["series_episodes"].InnerText != "" ? Int32.Parse(e["series_episodes"].InnerText) : 0;
			Status = e ["series_status"].InnerText != "" ? (AiringStatus)Int32.Parse(e ["series_status"].InnerText) : AiringStatus.None;
            AiringStart = StringToDate(e["series_start"].InnerText);
            AiringEnd = StringToDate(e["series_end"].InnerText);
			ImageUrl = e["series_image"].InnerText;
			MyID = e["my_id"].InnerText != "" ? Int32.Parse(e["my_id"].InnerText) : 0;
			MyWatchedEp = e["my_watched_episodes"].InnerText != "" ? Int32.Parse(e["my_watched_episodes"].InnerText) : 0;
			MyStartDate = StringToDate(e["my_start_date"].InnerText);
            MyFinishDate = StringToDate(e["my_finish_date"].InnerText);
			MyScore = e["my_score"].InnerText != "" ? Int32.Parse(e["my_score"].InnerText) : 0;
			MyWatchingStatus = e ["my_status"].InnerText != "" ? (WatchingStatus)Int32.Parse(e ["my_status"].InnerText) : WatchingStatus.None;
            MyRewatching = e["my_rewatching"].InnerText != "" ? Int32.Parse(e["my_rewatching_ep"].InnerText) : 0;
            MyRewatchingEpisodes = e["my_rewatching_ep"].InnerText != "" ? Int32.Parse(e["my_rewatching_ep"].InnerText) : 0;
			MyTags = e["my_tags"].InnerText;
		}

        public DateTime StringToDate(string date)
        {
                if (date != "0000-00-00" && date != "")
                {
                    try
                    {
                        return DateTime.ParseExact(date, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                    }
                    catch (FormatException)
                    {
                        try
                        {
                            date = date.Substring(0, 7);
                            return DateTime.ParseExact(date, "yyyy-MM", CultureInfo.InvariantCulture);
                        }
                        catch (FormatException)
                        {
                            try
                            {
                                date = date.Substring(0, 4);
                                return DateTime.ParseExact(date, "yyyy", CultureInfo.InvariantCulture);
                            }
                            catch (FormatException)
                            {
                                return DateTime.MinValue;
                            }
                        }
                    }
                }
                else
                    return DateTime.MinValue;
        }

        public override string ToString()
        {
            return Title;
        }
    }
}
