using System.Xml.Serialization;
using MiniMAL.Manga;

namespace MiniMAL.Internal
{
    [XmlRoot(ElementName = "entry", Namespace = "")]
    public class MangaRequestSerializable : RequestSerializable<MangaRequestData, ReadingStatus>
    {
        [XmlElement(ElementName = "chapter")]
        public int? Chapter { get; set; }

        [XmlElement(ElementName = "volume")]
        public int? Volume { get; set; }

        [XmlElement(ElementName = "downloaded_chapters")]
        public int? DownloadedChapters { get; set; }

        [XmlElement(ElementName = "times_reread")]
        public int? TimesReread { get; set; }

        [XmlElement(ElementName = "reread_value")]
        public int? RereadValue { get; set; }

        [XmlElement(ElementName = "enable_rereading")]
        public int? EnableRereading { get; set; }

        [XmlElement(ElementName = "scan_group")]
        public string ScanGroup { get; set; }

        [XmlElement(ElementName = "retail_volumes")]
        public int? RetailVolumes { get; set; }

        [XmlIgnore]
        public override bool StatusSpecified { get { return Status != ReadingStatus.None; } }

        [XmlIgnore]
        public bool ChapterSpecified { get { return Chapter.HasValue; } }

        [XmlIgnore]
        public bool VolumeSpecified { get { return Volume.HasValue; } }

        [XmlIgnore]
        public bool DownloadedChaptersSpecified { get { return DownloadedChapters.HasValue; } }

        [XmlIgnore]
        public bool TimesRereadSpecified { get { return TimesReread.HasValue; } }

        [XmlIgnore]
        public bool RereadValueSpecified { get { return RereadValue.HasValue; } }

        [XmlIgnore]
        public bool EnableRereadingSpecified { get { return EnableRereading.HasValue; } }

        [XmlIgnore]
        public bool ScanGroupSpecified { get { return ScanGroup == ""; } }

        [XmlIgnore]
        public bool RetailVolumesSpecified { get { return RetailVolumes.HasValue; } }

        public override void GetData(MangaRequestData data)
        {
            Status = data.Status;
            Score = data.Score;
            DateStart = data.DateStart;
            DateFinish = data.DateFinish;
            Priority = data.Priority;
            EnableDiscussion = data.EnableDiscussion;
            Comments = data.Comments;
            Tags = data.Tags;
            Chapter = data.Chapter;
            Volume = data.Volume;
            DownloadedChapters = data.DownloadedChapters;
            TimesReread = data.TimesReread;
            RereadValue = data.RereadValue;
            EnableRereading = data.EnableRereading;
            ScanGroup = data.ScanGroup;
            RetailVolumes = data.RetailVolumes;
        }
    }
}