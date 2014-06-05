using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Xml;

namespace MiniMAL
{
    public class AnimeSearchEntry : SearchEntry<TypeAnime, AiringStatus>
    {
        public int Episodes { get; set; }

        public override void LoadFromXmlNode(XmlNode e)
        {
            ID = e["id"].InnerText != "" ? Int32.Parse(e["id"].InnerText) : 0;
            Title = e["title"].InnerText;
            EnglishTitle = e["english"].InnerText;
            Synonyms = e["synonyms"].InnerText.Split(new string[] { "; " }, StringSplitOptions.RemoveEmptyEntries);
            Episodes = e["episodes"].InnerText != "" ? Int32.Parse(e["episodes"].InnerText) : 0;
            Score = e["score"].InnerText != "" ? Double.Parse(e["score"].InnerText, CultureInfo.InvariantCulture) : 0;
            Type = e["type"].InnerText != "" ? ParseType(e["type"].InnerText) : TypeAnime.None;
            Status = e["status"].InnerText != "" ? ParseStatus(e["status"].InnerText) : AiringStatus.None;
            StartDate = MiniMALTools.StringToDate(e["start_date"].InnerText);
            EndDate = MiniMALTools.StringToDate(e["end_date"].InnerText);
            Synopsis = e["synopsis"].InnerText;
            ImageUrl = e["image"].InnerText;
        }

        private TypeAnime ParseType(string text)
        {
            switch (text)
            {
                case "TV": return TypeAnime.TV;
                case "OVA": return TypeAnime.OVA;
                case "Movie": return TypeAnime.Movie;
                case "Special": return TypeAnime.Special;
                case "ONA": return TypeAnime.ONA;
                default: throw new InvalidOperationException();
            }
        }

        private AiringStatus ParseStatus(string text)
        {
            switch (text)
            {
                case "Airing": return AiringStatus.Airing;
                case "Finished Airing": return AiringStatus.Finished;
                case "Not yet airing": return AiringStatus.NoYetAiring;
                default: throw new InvalidOperationException();
            }
        }
    }
}
