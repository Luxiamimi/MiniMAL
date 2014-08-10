using System;
using System.Xml;
using MiniMAL.Generic;
using MiniMAL.Types;

namespace MiniMAL.Anime
{
    public class AnimeSearchEntry : SearchEntry<AnimeType, AiringStatus>
    {
        public int Episodes { get; protected set; }

        internal override void LoadFromXmlNode(XmlNode e)
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

        static private AnimeType ParseType(XmlNode xml)
        {
            switch (xml.InnerText)
            {
                case "":
                    return AnimeType.None;
                case "TV":
                    return AnimeType.TV;
                case "OVA":
                    return AnimeType.OVA;
                case "Movie":
                    return AnimeType.Movie;
                case "Special":
                    return AnimeType.Special;
                case "ONA":
                    return AnimeType.ONA;
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