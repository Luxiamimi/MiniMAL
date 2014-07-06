using System;
using System.Xml;
using MiniMAL.Internal;

namespace MiniMAL
{
    public class AnimeSearchEntry : SearchEntry<TypeAnime, AiringStatus>
    {
        public int Episodes { get; protected set; }

        public void LoadFromXmlNode(XmlNode e)
        {
            Id = MiniMALConverter.XmlToInt(e["id"]);
            Title = MiniMALConverter.XmlToString(e["title"]);
            EnglishTitle = MiniMALConverter.XmlToString(e["english"]);
            Synonyms = MiniMALConverter.XmlToString(e["synonyms"]).
                                        Split(new[] {"; "}, StringSplitOptions.RemoveEmptyEntries);
            Episodes = MiniMALConverter.XmlToInt(e["episodes"]);
            Score = MiniMALConverter.XmlToDouble(e["score"]);
            Type = ParseType(e["type"]);
            Status = ParseStatus(e["status"]);
            StartDate = MiniMALConverter.XmlToDate(e["start_date"]);
            EndDate = MiniMALConverter.XmlToDate(e["end_date"]);
            Synopsis = MiniMALConverter.XmlToString(e["synopsis"]);
            ImageUrl = MiniMALConverter.XmlToString(e["image"]);
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