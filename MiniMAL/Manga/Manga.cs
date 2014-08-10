using System;
using System.Xml;
using MiniMAL.Generic;
using MiniMAL.Types;

namespace MiniMAL.Manga
{
    public class Manga : Entry<MangaType, PublishingStatus, ReadingStatus>
    {
        public int Chapters { get; set; }
        public int Volumes { get; set; }
        public int MyReadChapters { get; set; }
        public int MyReadVolumes { get; set; }
        public int MyRereadingCount { get; set; }
        public int MyRereadingChapters { get; set; }

        internal override void LoadFromXmlNode(XmlNode e)
        {
            Id = MALConverter.XmlToInt(e["series_mangadb_id"]);
            Title = MALConverter.XmlToString(e["series_title"]);
            Synonyms = MALConverter.XmlToString(e["series_synonyms"]).
                                    Split(new[] {"; "}, StringSplitOptions.RemoveEmptyEntries);
            Type = MALConverter.XmlToString(e["series_type"]) != ""
                       ? (MangaType)Int32.Parse(MALConverter.XmlToString(e["series_type"]))
                       : MangaType.None;
            Chapters = MALConverter.XmlToInt(e["series_chapters"]);
            Volumes = MALConverter.XmlToInt(e["series_volumes"]);
            Status = MALConverter.XmlToString(e["series_status"]) != ""
                         ? (PublishingStatus)Int32.Parse(MALConverter.XmlToString(e["series_status"]))
                         : PublishingStatus.None;
            StartDate = MALConverter.XmlToDate(e["series_start"]);
            EndDate = MALConverter.XmlToDate(e["series_end"]);
            ImageUrl = MALConverter.XmlToString(e["series_image"]);
            MyId = MALConverter.XmlToInt(e["my_id"]);
            MyReadChapters = MALConverter.XmlToInt(e["my_read_chapters"]);
            MyReadVolumes = MALConverter.XmlToInt(e["my_read_volumes"]);
            MyStartDate = MALConverter.XmlToDate(e["my_start_date"]);
            MyEndDate = MALConverter.XmlToDate(e["my_finish_date"]);
            MyScore = MALConverter.XmlToInt(e["my_score"]);
            MyStatus = MALConverter.XmlToString(e["my_status"]) != ""
                           ? (ReadingStatus)Int32.Parse(MALConverter.XmlToString(e["my_status"]))
                           : ReadingStatus.None;
            MyRereadingCount = MALConverter.XmlToInt(e["my_rereadingg"]);
            MyRereadingChapters = MALConverter.XmlToInt(e["my_rereading_chap"]);
            MyTags = MALConverter.XmlToString(e["my_tags"]).Split(',');
        }
    }
}