using System;
using System.Xml;
using MiniMAL.Internal;

namespace MiniMAL
{
    public class Manga : Entry<TypeManga, PublishingStatus, ReadingStatus>
    {
        public int Chapters { get; set; }
        public int Volumes { get; set; }
        public int MyReadChapters { get; set; }
        public int MyReadVolumes { get; set; }
        public int MyRereadingCount { get; set; }
        public int MyRereadingChapters { get; set; }

        public void LoadFromXmlNode(XmlNode e)
        {
            Id = MiniMALConverter.XmlToInt(e["series_mangadb_id"]);
            Title = MiniMALConverter.XmlToString(e["series_title"]);
            Synonyms = MiniMALConverter.XmlToString(e["series_synonyms"]).Split(new[] { "; " }, StringSplitOptions.RemoveEmptyEntries);
            Type = MiniMALConverter.XmlToString(e["series_type"]) != "" ? (TypeManga)Int32.Parse(MiniMALConverter.XmlToString(e["series_type"])) : TypeManga.None;
            Chapters = MiniMALConverter.XmlToInt(e["series_chapters"]);
            Volumes = MiniMALConverter.XmlToInt(e["series_volumes"]);
            Status = MiniMALConverter.XmlToString(e["series_status"]) != "" ? (PublishingStatus)Int32.Parse(MiniMALConverter.XmlToString(e["series_status"])) : PublishingStatus.None;
            StartDate = MiniMALConverter.XmlToDate(e["series_start"]);
            EndDate = MiniMALConverter.XmlToDate(e["series_end"]);
            ImageUrl = MiniMALConverter.XmlToString(e["series_image"]);
            MyId = MiniMALConverter.XmlToInt(e["my_id"]);
            MyReadChapters = MiniMALConverter.XmlToInt(e["my_read_chapters"]);
            MyReadVolumes = MiniMALConverter.XmlToInt(e["my_read_volumes"]);
            MyStartDate = MiniMALConverter.XmlToDate(e["my_start_date"]);
            MyEndDate = MiniMALConverter.XmlToDate(e["my_finish_date"]);
            MyScore = MiniMALConverter.XmlToInt(e["my_score"]);
            MyStatus = MiniMALConverter.XmlToString(e["my_status"]) != "" ? (ReadingStatus)Int32.Parse(MiniMALConverter.XmlToString(e["my_status"])) : ReadingStatus.None;
            MyRereadingCount = MiniMALConverter.XmlToInt(e["my_rereadingg"]);
            MyRereadingChapters = MiniMALConverter.XmlToInt(e["my_rereading_chap"]);
            MyTags = MiniMALConverter.XmlToString(e["my_tags"]).Split(',');
        }
    }
}