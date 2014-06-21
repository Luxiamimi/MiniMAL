using System;
using System.Xml;

namespace MiniMAL
{
    public class MangaSearchEntry : SearchEntry<TypeManga, PublishingStatus>
    {
        public int Chapters { get; set; }
        public int Volumes { get; set; }

        public override void LoadFromXmlNode(XmlNode e)
        {
            ID = MiniMALConverter.XmlToInt(e["id"]);
            Title = e["title"].InnerText;
            EnglishTitle = e["english"].InnerText;
            Synonyms = e["synonyms"].InnerText.Split(new string[] { "; " }, StringSplitOptions.RemoveEmptyEntries);
            Chapters = MiniMALConverter.XmlToInt(e["chapters"]);
            Volumes = MiniMALConverter.XmlToInt(e["volumes"]);
            Score = MiniMALConverter.XmlToDouble(e["score"]);
            Type = ParseType(e["type"]);
            Status = ParseStatus(e["status"]);
            StartDate = MiniMALConverter.XmlToDate(e["start_date"]);
            EndDate = MiniMALConverter.XmlToDate(e["end_date"]);
            Synopsis = e["synopsis"].InnerText;
            ImageUrl = e["image"].InnerText;
        }

        private TypeManga ParseType(XmlNode xml)
        {
            switch (xml.InnerText)
            {
                case "": return TypeManga.None;
                case "Manga": return TypeManga.Manga;
                case "Novel": return TypeManga.Novel;
                case "One Shot": return TypeManga.OneShot;
                case "Doujin": return TypeManga.Doujin;
                case "Manhwa": return TypeManga.Manhwa;
                case "Manhua": return TypeManga.Manhua;
                default: throw new InvalidOperationException();
            }
        }

        private PublishingStatus ParseStatus(XmlNode xml)
        {
            switch (xml.InnerText)
            {
                case "": return PublishingStatus.None;
                case "Publishing": return PublishingStatus.Publishing;
                case "Finished": return PublishingStatus.Finished;
                case "Not yet publishing": return PublishingStatus.NoYetPublished;
                default: throw new InvalidOperationException();
            }
        }
    }
}