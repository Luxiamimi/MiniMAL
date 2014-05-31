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
            ID = e["series_mangadb_id"].InnerText != "" ? Int32.Parse(e["series_mangadb_id"].InnerText) : 0;
            Title = e["series_title"].InnerText;
            Synonyms = e["series_synonyms"].InnerText.Split(new string[] { "; " }, StringSplitOptions.RemoveEmptyEntries);
            Type = e["series_type"].InnerText != "" ? (TypeManga)Int32.Parse(e["series_type"].InnerText) : TypeManga.None;
            Chapters = e["series_chapters"].InnerText != "" ? Int32.Parse(e["series_chapters"].InnerText) : 0;
            Volumes = e["series_volumes"].InnerText != "" ? Int32.Parse(e["series_volumes"].InnerText) : 0;
            Status = e["series_status"].InnerText != "" ? (PublishingStatus)Int32.Parse(e["series_status"].InnerText) : PublishingStatus.None;
            StartDate = MiniMALTools.StringToDate(e["series_start"].InnerText);
            EndDate = MiniMALTools.StringToDate(e["series_end"].InnerText);
            ImageUrl = e["series_image"].InnerText;
            MyID = e["my_id"].InnerText != "" ? Int32.Parse(e["my_id"].InnerText) : 0;
            MyReadChapters = e["my_read_chapters"].InnerText != "" ? Int32.Parse(e["my_read_chapters"].InnerText) : 0;
            MyReadVolumes = e["my_read_volumes"].InnerText != "" ? Int32.Parse(e["my_read_volumes"].InnerText) : 0;
            MyStartDate = MiniMALTools.StringToDate(e["my_start_date"].InnerText);
            MyEndDate = MiniMALTools.StringToDate(e["my_finish_date"].InnerText);
            MyScore = e["my_score"].InnerText != "" ? Int32.Parse(e["my_score"].InnerText) : 0;
            MyStatus = e["my_status"].InnerText != "" ? (ReadingStatus)Int32.Parse(e["my_status"].InnerText) : ReadingStatus.None;
            MyRereadingCount = e["my_rereadingg"].InnerText != "" ? Int32.Parse(e["my_rereadingg"].InnerText) : 0;
            MyRereadingChapters = e["my_rereading_chap"].InnerText != "" ? Int32.Parse(e["my_rereading_chap"].InnerText) : 0;
            MyTags = e["my_tags"].InnerText;
		}
    }
}
