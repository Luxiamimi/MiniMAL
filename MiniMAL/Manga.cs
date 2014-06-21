using System;
using System.Xml;

namespace MiniMAL
{
    public enum TypeManga
    {
        None = 0, Manga = 1, Novel = 2, OneShot = 3, Doujin = 4, Manhwa = 5, Manhua = 6
    }

    public enum PublishingStatus
    {
        None = 0, Publishing = 1, Finished = 2, NoYetPublished = 3
    }

    public enum ReadingStatus
    {
        None = 0, Reading = 1, Completed = 2, OnHold = 3, Dropped = 4, PlanToRead = 6
    }

    public class Manga : Entry<TypeManga, PublishingStatus, ReadingStatus>
    {
        public int Chapters { get; set; }
        public int Volumes { get; set; }
        public int MyReadChapters { get; set; }
        public int MyReadVolumes { get; set; }
        public int MyRereadingCount { get; set; }
        public int MyRereadingChapters { get; set; }

        public override void LoadFromXmlNode(XmlNode e)
        {
            ID = MiniMALConverter.XmlToInt(e["series_mangadb_id"]);
            Title = e["series_title"].InnerText;
            Synonyms = e["series_synonyms"].InnerText.Split(new string[] { "; " }, StringSplitOptions.RemoveEmptyEntries);
            Type = e["series_type"].InnerText != "" ? (TypeManga)Int32.Parse(e["series_type"].InnerText) : TypeManga.None;
            Chapters = MiniMALConverter.XmlToInt(e["series_chapters"]);
            Volumes = MiniMALConverter.XmlToInt(e["series_volumes"]);
            Status = e["series_status"].InnerText != "" ? (PublishingStatus)Int32.Parse(e["series_status"].InnerText) : PublishingStatus.None;
            StartDate = MiniMALConverter.XmlToDate(e["series_start"]);
            EndDate = MiniMALConverter.XmlToDate(e["series_end"]);
            ImageUrl = e["series_image"].InnerText;
            MyID = MiniMALConverter.XmlToInt(e["my_id"]);
            MyReadChapters = MiniMALConverter.XmlToInt(e["my_read_chapters"]);
            MyReadVolumes = MiniMALConverter.XmlToInt(e["my_read_volumes"]);
            MyStartDate = MiniMALConverter.XmlToDate(e["my_start_date"]);
            MyEndDate = MiniMALConverter.XmlToDate(e["my_finish_date"]);
            MyScore = MiniMALConverter.XmlToInt(e["my_score"]);
            MyStatus = e["my_status"].InnerText != "" ? (ReadingStatus)Int32.Parse(e["my_status"].InnerText) : ReadingStatus.None;
            MyRereadingCount = MiniMALConverter.XmlToInt(e["my_rereadingg"]);
            MyRereadingChapters = MiniMALConverter.XmlToInt(e["my_rereading_chap"]);
            MyTags = e["my_tags"].InnerText;
        }
    }
}