using System;
using System.Xml;
using MiniMAL.Internal;

namespace MiniMAL
{
    public class Anime : Entry<TypeAnime, AiringStatus, WatchingStatus>
    {
        public int Episodes { get; set; }
        public int MyWatchedEpisodes { get; set; }
        public int MyRewatchingCount { get; set; }
        public int MyRewatchingEpisodes { get; set; }

        public void LoadFromXmlNode(XmlNode e)
        {
            Id = MALConverter.XmlToInt(e["series_animedb_id"]);
            Title = MALConverter.XmlToString(e["series_title"]);
            Synonyms = MALConverter.XmlToString(e["series_synonyms"]).
                                    Split(new[] {"; "}, StringSplitOptions.RemoveEmptyEntries);
            Type = MALConverter.XmlToString(e["series_type"]) != ""
                       ? (TypeAnime)Int32.Parse(MALConverter.XmlToString(e["series_type"]))
                       : TypeAnime.None;
            Episodes = MALConverter.XmlToInt(e["series_episodes"]);
            Status = MALConverter.XmlToString(e["series_status"]) != ""
                         ? (AiringStatus)Int32.Parse(MALConverter.XmlToString(e["series_status"]))
                         : AiringStatus.None;
            StartDate = MALConverter.XmlToDate(e["series_start"]);
            EndDate = MALConverter.XmlToDate(e["series_end"]);
            ImageUrl = MALConverter.XmlToString(e["series_image"]);
            MyId = MALConverter.XmlToInt(e["my_id"]);
            MyWatchedEpisodes = MALConverter.XmlToInt(e["my_watched_episodes"]);
            MyStartDate = MALConverter.XmlToDate(e["my_start_date"]);
            MyEndDate = MALConverter.XmlToDate(e["my_finish_date"]);
            MyScore = MALConverter.XmlToInt(e["my_score"]);
            MyStatus = MALConverter.XmlToString(e["my_status"]) != ""
                           ? (WatchingStatus)Int32.Parse(MALConverter.XmlToString(e["my_status"]))
                           : WatchingStatus.None;
            MyRewatchingCount = MALConverter.XmlToInt(e["my_rewatching"]);
            MyRewatchingEpisodes = MALConverter.XmlToInt(e["my_rewatching_ep"]);
            MyTags = MALConverter.XmlToString(e["my_tags"]).Split(',');
        }
    }
}