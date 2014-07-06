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
            Id = MiniMALConverter.XmlToInt(e["series_animedb_id"]);
            Title = MiniMALConverter.XmlToString(e["series_title"]);
            Synonyms = MiniMALConverter.XmlToString(e["series_synonyms"]).
                                        Split(new[] {"; "}, StringSplitOptions.RemoveEmptyEntries);
            Type = MiniMALConverter.XmlToString(e["series_type"]) != ""
                       ? (TypeAnime)Int32.Parse(MiniMALConverter.XmlToString(e["series_type"]))
                       : TypeAnime.None;
            Episodes = MiniMALConverter.XmlToInt(e["series_episodes"]);
            Status = MiniMALConverter.XmlToString(e["series_status"]) != ""
                         ? (AiringStatus)
                           Int32.Parse(MiniMALConverter.XmlToString(e["series_status"]))
                         : AiringStatus.None;
            StartDate = MiniMALConverter.XmlToDate(e["series_start"]);
            EndDate = MiniMALConverter.XmlToDate(e["series_end"]);
            ImageUrl = MiniMALConverter.XmlToString(e["series_image"]);
            MyId = MiniMALConverter.XmlToInt(e["my_id"]);
            MyWatchedEpisodes = MiniMALConverter.XmlToInt(e["my_watched_episodes"]);
            MyStartDate = MiniMALConverter.XmlToDate(e["my_start_date"]);
            MyEndDate = MiniMALConverter.XmlToDate(e["my_finish_date"]);
            MyScore = MiniMALConverter.XmlToInt(e["my_score"]);
            MyStatus = MiniMALConverter.XmlToString(e["my_status"]) != ""
                           ? (WatchingStatus)
                             Int32.Parse(MiniMALConverter.XmlToString(e["my_status"]))
                           : WatchingStatus.None;
            MyRewatchingCount = MiniMALConverter.XmlToInt(e["my_rewatching"]);
            MyRewatchingEpisodes = MiniMALConverter.XmlToInt(e["my_rewatching_ep"]);
            MyTags = MiniMALConverter.XmlToString(e["my_tags"]).Split(',');
        }
    }
}