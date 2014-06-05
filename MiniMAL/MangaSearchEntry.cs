using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace MiniMAL
{
    public class MangaSearchEntry : SearchEntry<TypeManga, PublishingStatus>
    {
        public int Chapters { get; set; }
        public int Volumes { get; set; }

        public override void LoadFromXmlNode(XmlNode e)
        {
            ID = e["id"].InnerText != "" ? Int32.Parse(e["id"].InnerText) : 0;
            Title = e["title"].InnerText;
            EnglishTitle = e["english"].InnerText;
            Synonyms = e["synonyms"].InnerText.Split(new string[] { "; " }, StringSplitOptions.RemoveEmptyEntries);
            Chapters = e["episodes"].InnerText != "" ? Int32.Parse(e["episodes"].InnerText) : 0;
            Volumes = e["episodes"].InnerText != "" ? Int32.Parse(e["episodes"].InnerText) : 0;
            Score = e["score"].InnerText != "" ? Double.Parse(e["score"].InnerText) : 0;
            Type = e["type"].InnerText != "" ? (TypeManga)Enum.Parse(typeof(TypeManga), e["type"].InnerText, true) : TypeManga.None;
            Status = e["status"].InnerText != "" ? (PublishingStatus)Enum.Parse(typeof(PublishingStatus), e["status"].InnerText, true) : PublishingStatus.None;
            StartDate = MiniMALTools.StringToDate(e["start_date"].InnerText);
            EndDate = MiniMALTools.StringToDate(e["end_date"].InnerText);
            Synopsis = e["synopsis"].InnerText;
            ImageUrl = e["image"].InnerText;
        }
    }
}
