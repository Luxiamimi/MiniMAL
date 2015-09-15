using System;
using System.Xml;
using MiniMAL.Types;

namespace MiniMAL.Generic
{
    // TODO : Reduce generic impact
    public abstract class Entry<TSeriesType, TSeriesStatus, TMyStatus>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string[] Synonyms { get; set; }
        public TSeriesType Type { get; set; }
        public TSeriesStatus Status { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string ImageUrl { get; set; }
        public int MyId { get; set; }
        public DateTime MyStartDate { get; set; }
        public DateTime MyEndDate { get; set; }
        public int MyScore { get; set; }
        public TMyStatus MyStatus { get; set; }
        public MALTags MyTags { get; set; }

        public string TitleForUrl
        {
            get { return Title.Replace(" ", "_").Replace("/", "_").Replace("?", "").Replace(",", "").Replace("\x27", ""); }
        }

        internal Entry()
        {
        }

        public override string ToString()
        {
            return Title;
        }

        internal abstract void LoadFromXmlNode(XmlNode e);
    }
}