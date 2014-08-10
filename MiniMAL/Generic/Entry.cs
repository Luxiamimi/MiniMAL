using System;
using System.Xml;
using MiniMAL.Types;

namespace MiniMAL.Generic
{
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
            get
            {
                string result = Title.Replace(" ", "_").Replace("?", "").Replace(",", "").Replace("\x27", "");
                return String.Join("_", result.Split(new[] {'_'}, StringSplitOptions.RemoveEmptyEntries));
            }
        }

        internal Entry() {}

        internal abstract void LoadFromXmlNode(XmlNode e);

        public override string ToString()
        {
            return Title;
        }
    }
}