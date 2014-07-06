using System;
using System.Xml;
using MiniMAL.Internal;

namespace MiniMAL
{
    public class MangaSearchEntry : SearchEntry<TypeManga, PublishingStatus>
    {
        public int Chapters { get; protected set; }
        public int Volumes { get; protected set; }

        public void LoadFromXmlNode(XmlNode e)
        {
            Id = MALConverter.XmlToInt(e["id"]);
            Title = MALConverter.XmlToString(e["title"]);
            EnglishTitle = MALConverter.XmlToString(e["english"]);
            Synonyms = MALConverter.XmlToString(e["synonyms"]).
                                    Split(new[] {"; "}, StringSplitOptions.RemoveEmptyEntries);
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

        static private TypeManga ParseType(XmlNode xml)
        {
            switch (xml.InnerText)
            {
                case "":
                    return TypeManga.None;
                case "Manga":
                    return TypeManga.Manga;
                case "Novel":
                    return TypeManga.Novel;
                case "One Shot":
                    return TypeManga.OneShot;
                case "Doujin":
                    return TypeManga.Doujin;
                case "Manhwa":
                    return TypeManga.Manhwa;
                case "Manhua":
                    return TypeManga.Manhua;
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