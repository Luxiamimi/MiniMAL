using System;
using System.Xml;

namespace MiniMAL.Internal
{
    public abstract class Entry<TSeriesType, TSeriesStatus, TMyStatus>
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string[] Synonyms { get; set; }
        public TSeriesType Type { get; set; }
        public TSeriesStatus Status { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string ImageUrl { get; set; }
        public int MyID { get; set; }
        public DateTime MyStartDate { get; set; }
        public DateTime MyEndDate { get; set; }
        public int MyScore { get; set; }
        public TMyStatus MyStatus { get; set; }
        public MALTags MyTags { get; set; }

        internal Entry()
        {
        }

        public abstract void LoadFromXmlNode(XmlNode e);

        public override string ToString()
        {
            return Title;
        }

        public string TitleForUrl
        {
            get
            {
                string result = Title.Replace(" ", "_").Replace("?", "").Replace(",", "").Replace("\x27", "");
                return String.Join("_", result.Split(new char[] { '_' }
                    , StringSplitOptions.RemoveEmptyEntries));
            }
        }
    }
}