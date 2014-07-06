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
            Id = MiniMALConverter.XmlToInt(e["id"]);
            Title = MiniMALConverter.XmlToString(e["title"]);
            EnglishTitle = MiniMALConverter.XmlToString(e["english"]);
            Synonyms = MiniMALConverter.XmlToString(e["synonyms"]).
                                        Split(new[] {"; "}, StringSplitOptions.RemoveEmptyEntries);
            Chapters = MiniMALConverter.XmlToInt(e["chapters"]);
            Volumes = MiniMALConverter.XmlToInt(e["volumes"]);
            Score = MiniMALConverter.XmlToDouble(e["score"]);
            Type = ParseType(e["type"]);
            Status = ParseStatus(e["status"]);
            StartDate = MiniMALConverter.XmlToDate(e["start_date"]);
            EndDate = MiniMALConverter.XmlToDate(e["end_date"]);
            Synopsis = MiniMALConverter.XmlToString(e["synopsis"]);
            ImageUrl = MiniMALConverter.XmlToString(e["image"]);
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