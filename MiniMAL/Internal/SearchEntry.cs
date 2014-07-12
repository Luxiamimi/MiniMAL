﻿using System;
using System.Xml;
using MiniMAL.Internal.Interfaces;

namespace MiniMAL.Internal
{
    public abstract class SearchEntry<TSeriesType, TSeriesStatus> : ISearchEntry
    {
        public int Id { get; protected set; }
        public string Title { get; protected set; }
        public string EnglishTitle { get; protected set; }
        public string[] Synonyms { get; protected set; }
        public double Score { get; protected set; }
        public TSeriesType Type { get; protected set; }
        public TSeriesStatus Status { get; protected set; }
        public DateTime StartDate { get; protected set; }
        public DateTime EndDate { get; protected set; }
        public string Synopsis { get; protected set; }
        public string ImageUrl { get; protected set; }

        public string TitleForUrl
        {
            get
            {
                string result = Title.Replace(" ", "_").Replace("?", "").Replace(",", "").Replace("\x27", "");
                return String.Join("_", result.Split(new[] {'_'}, StringSplitOptions.RemoveEmptyEntries));
            }
        }

        internal SearchEntry() {}

        public abstract void LoadFromXmlNode(XmlNode e);

        public override string ToString()
        {
            return Title;
        }
    }
}