using System;
using System.Xml;
using MiniMAL.Internal;

namespace MiniMAL.Anime
{
    public class AnimeSearchEntry : SearchEntry<TypeAnime, AiringStatus>
    {
        public int Episodes { get; protected set; }

        public override void LoadFromXmlNode(XmlNode e)
        {
            Id = MALConverter.XmlToInt(e["id"]);
            Title = MALConverter.XmlToString(e["title"]);
            EnglishTitle = MALConverter.XmlToString(e["english"]);
            Synonyms = MALConverter.XmlToString(e["synonyms"]).
                                    Split(new[] {"; "}, StringSplitOptions.RemoveEmptyEntries);
            Episodes = MALConverter.XmlToInt(e["episodes"]);
            Score = MALConverter.XmlToDouble(e["score"]);
            Type = ParseType(e["type"]);
            Status = ParseStatus(e["status"]);
            StartDate = MALConverter.XmlToDate(e["start_date"]);
            EndDate = MALConverter.XmlToDate(e["end_date"]);
            Synopsis = MALConverter.XmlToString(e["synopsis"]);
            ImageUrl = MALConverter.XmlToString(e["image"]);
        }

        static private TypeAnime ParseType(XmlNode xml)
        {
            switch (xml.InnerText)
            {
                case "":
                    return TypeAnime.None;
                case "TV":
                    return TypeAnime.TV;
                case "OVA":
                    return TypeAnime.OVA;
                case "Movie":
                    return TypeAnime.Movie;
                case "Special":
                    return TypeAnime.Special;
                case "ONA":
                    return TypeAnime.ONA;
                default:
                    throw new InvalidOperationException();
            }
        }

        static private AiringStatus ParseStatus(XmlNode xml)
        {
            switch (xml.InnerText)
            {
                case "":
                    return AiringStatus.None;
                case "Currently Airing":
                    return AiringStatus.Airing;
                case "Finished Airing":
                    return AiringStatus.Finished;
                case "Not yet aired":
                    return AiringStatus.NoYetAiring;
                default:
                    throw new InvalidOperationException();
            }
        }
    }
}