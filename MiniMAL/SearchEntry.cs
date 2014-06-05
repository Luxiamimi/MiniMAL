using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace MiniMAL
{
    public abstract class SearchEntry<TSeriesType, TSeriesStatus>
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string EnglishTitle { get; set; }
        public string[] Synonyms { get; set; }
        public double Score { get; set; }
        public TSeriesType Type { get; set; }
        public TSeriesStatus Status { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Synopsis { get; set; }
        public string ImageUrl { get; set; }

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
