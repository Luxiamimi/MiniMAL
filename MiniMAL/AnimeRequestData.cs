using System;
using System.Xml.Serialization;
using MiniMAL.Internal;

namespace MiniMAL
{
    [XmlRoot(ElementName = "entry", Namespace = "")]
    public class AnimeRequestData : EntryRequestData
    {
        [XmlElement(ElementName = "episode")]
        public int? Episode { get; set; }
        [XmlElement(ElementName = "downloaded_episodes")]
        public int? DownloadedEpisodes { get; set; }
        [XmlElement(ElementName = "storage_type")]
        public int? StorageType { get; set; }
        [XmlElement(ElementName = "storage_value")]
        public float? StorageValue { get; set; }
        [XmlElement(ElementName = "times_rewatched")]
        public int? TimesRewatched { get; set; }
        [XmlElement(ElementName = "rewatch_value")]
        public int? RewatchValue { get; set; }
        [XmlElement(ElementName = "enable_rewatching")]
        public int? EnableRewatching { get; set; }
        [XmlElement(ElementName = "fansub_group")]
        public string FansubGroup { get; set; }

        [XmlIgnore]
        public bool EpisodeSpecified { get { return Episode.HasValue; } }
        [XmlIgnore]
        public bool DownloadedEpisodesSpecified { get { return DownloadedEpisodes.HasValue; } }
        [XmlIgnore]
        public bool StorageTypeSpecified { get { return StorageType.HasValue; } }
        [XmlIgnore]
        public bool StorageValueSpecified { get { return StorageValue.HasValue; } }
        [XmlIgnore]
        public bool TimesRewatchedSpecified { get { return TimesRewatched.HasValue; } }
        [XmlIgnore]
        public bool RewatchValueSpecified { get { return RewatchValue.HasValue; } }
        [XmlIgnore]
        public bool EnableRewatchingSpecified { get { return EnableRewatching.HasValue; } }
        [XmlIgnore]
        public bool FansubGroupSpecified { get { return FansubGroup == ""; } }

        public AnimeRequestData()
        {
        }

        public AnimeRequestData(Anime a)
        {
            Episode = a.MyWatchedEpisodes;
            Status = (int)a.MyStatus;
            Score = a.MyScore;
            DateStart = a.MyStartDate;
            DateFinish = a.MyEndDate;
            Tags = a.MyTags;
        }

        public static AnimeRequestData DefaultAddRequest(WatchingStatus status)
        {
            var result = new AnimeRequestData {Status = (int) status, Episode = 1, Score = 0, DateStart = DateTime.Now};
            return result;
        }
    }
}