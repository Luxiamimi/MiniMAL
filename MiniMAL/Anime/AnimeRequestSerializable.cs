using System.Xml.Serialization;
using MiniMAL.Generic;

namespace MiniMAL.Anime
{
    [XmlRoot(ElementName = "entry", Namespace = "")]
    internal class AnimeRequestSerializable : RequestSerializable<AnimeRequestData, WatchingStatus>
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
        public override bool StatusSpecified { get { return Status != WatchingStatus.None; } }

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

        public override void GetData(AnimeRequestData data)
        {
            Status = data.Status;
            Score = data.Score;
            DateStart = data.DateStart;
            DateFinish = data.DateFinish;
            Priority = data.Priority;
            EnableDiscussion = data.EnableDiscussion;
            Comments = data.Comments;
            Tags = data.Tags;
            Episode = data.Episode;
            DownloadedEpisodes = data.DownloadedEpisodes;
            StorageType = data.StorageType;
            StorageValue = data.StorageValue;
            TimesRewatched = data.TimesRewatched;
            RewatchValue = data.RewatchValue;
            EnableRewatching = data.EnableRewatching;
            FansubGroup = data.FansubGroup;
        }
    }
}