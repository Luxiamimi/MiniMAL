using System;
using System.Xml;
using MiniMAL.Generic;
using MiniMAL.Types;

namespace MiniMAL.Manga
{
    public class MangaSearchEntry : SearchEntry<MangaType, PublishingStatus>
    {
        public int Chapters { get; protected set; }
        public int Volumes { get; protected set; }

        internal override void LoadFromXmlNode(XmlNode e)
        {
            Id = MALConverter.XmlToInt(e["id"]);
            Title = MALConverter.XmlToString(e["title"]);
            EnglishTitle = MALConverter.XmlToString(e["english"]);
            Synonyms = MALConverter.XmlToString(e["synonyms"]).
                Split(new[]
                {
                    "; "
                }, StringSplitOptions.RemoveEmptyEntries);
            Chapters = MALConverter.XmlToInt(e["chapters"]);
            Volumes = MALConverter.XmlToInt(e["volumes"]);
            Score = MALConverter.XmlToDouble(e["score"]);
            Type = ParseType(e["type"]);
            Status = ParseStatus(e["status"]);
            StartDate = MALConverter.XmlToDate(e["start_date"]);
            EndDate = MALConverter.XmlToDate(e["end_date"]);
            Synopsis = MALConverter.XmlToString(e["synopsis"]);
            ImageUrl = MALConverter.XmlToString(e["image"]);
        }

        static private MangaType ParseType(XmlNode xml)
        {
            switch (xml.InnerText)
            {
                case "":
                    return MangaType.None;
                case "Manga":
                    return MangaType.Manga;
                case "Novel":
                    return MangaType.Novel;
                case "One Shot":
                    return MangaType.OneShot;
                case "Doujin":
                    return MangaType.Doujin;
                case "Manhwa":
                    return MangaType.Manhwa;
                case "Manhua":
                    return MangaType.Manhua;
                default:
                    throw new InvalidOperationException();
            }
        }

        static private PublishingStatus ParseStatus(XmlNode xml)
        {
            switch (xml.InnerText)
            {
                case "":
                    return PublishingStatus.None;
                case "Publishing":
                    return PublishingStatus.Publishing;
                case "Finished":
                    return PublishingStatus.Finished;
                case "Not yet publishing":
                    return PublishingStatus.NoYetPublished;
                default:
                    throw new InvalidOperationException();
            }
        }
    }
}