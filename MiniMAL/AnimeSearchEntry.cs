using System;
using System.Xml;
using MiniMAL.Internal;

namespace MiniMAL
{
    public class AnimeSearchEntry : SearchEntry<TypeAnime, AiringStatus>
    {
        public int Episodes { get; set; }

        public override void LoadFromXmlNode(XmlNode e)
        {
            ID = MiniMALConverter.XmlToInt(e["id"]);
            Title = e["title"].InnerText;
            EnglishTitle = e["english"].InnerText;
            Synonyms = e["synonyms"].InnerText.Split(new string[] { "; " }, StringSplitOptions.RemoveEmptyEntries);
            Episodes = MiniMALConverter.XmlToInt(e["episodes"]);
            Score = MiniMALConverter.XmlToDouble(e["score"]);
            Type = ParseType(e["type"]);
            Status = ParseStatus(e["status"]);
            StartDate = MiniMALConverter.XmlToDate(e["start_date"]);
            EndDate = MiniMALConverter.XmlToDate(e["end_date"]);
            Synopsis = e["synopsis"].InnerText;
            ImageUrl = e["image"].InnerText;
        }

        private TypeAnime ParseType(XmlNode xml)
        {
            switch (xml.InnerText)
            {
                case "": return TypeAnime.None;
                case "TV": return TypeAnime.TV;
                case "OVA": return TypeAnime.OVA;
                case "Movie": return TypeAnime.Movie;
                case "Special": return TypeAnime.Special;
                case "ONA": return TypeAnime.ONA;
                default: throw new InvalidOperationException();
            }
        }

        private AiringStatus ParseStatus(XmlNode xml)
        {
            switch (xml.InnerText)
            {
                case "": return AiringStatus.None;
                case "Currently Airing": return AiringStatus.Airing;
                case "Finished Airing": return AiringStatus.Finished;
                case "Not yet aired": return AiringStatus.NoYetAiring;
                default: throw new InvalidOperationException();
            }
        }
    }
}