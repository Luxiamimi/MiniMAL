using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Xml;

namespace MiniMAL
{
    public class Manga
    {
		public enum TypeManga
        {
			None = 0, Manga = 1, Novel = 2, OneShot = 3, Doujin = 4, Manhwa = 5, Manhua = 6
        }

        public enum PublishingStatus
        {
			None = 0, Publishing = 1, Finished = 2, NoYetPublished = 3
        }

        public enum ReadingStatus
        {
            None = 0, Reading = 1, Completed = 2, OnHold = 3, Dropped = 4, PlanToRead = 6
        }

        public int ID { get; set; }
        public string Title { get; set; }
        public string[] Synonyms { get; set; }
		public TypeManga Type { get; set; }
        public int Chapters { get; set; }
        public int Volumes { get; set; }
        public PublishingStatus Status { get; set; }
        public DateTime PublishingStart { get; set; }
        public DateTime PublishingEnd { get; set; }
        public string ImageUrl { get; set; }
        public int MyID { get; set; }
        public int MyReadChapters { get; set; }
        public int MyReadVolumes { get; set; }
        public DateTime MyStartDate { get; set; }
        public DateTime MyFinishDate { get; set; }
        public int MyScore { get; set; }
        public ReadingStatus MyReadingStatus { get; set; }
        public int MyRereading { get; set; }
        public int MyRereadingChapters { get; set; }
		public string MyTags { get; set; }

		public string TitleForUrl
		{
			get
			{
				string result = Title.Replace(" ", "_").Replace("?", "").Replace(",", "").Replace("\x27", "");
				return String.Join("_", result.Split(new char[] { '_' }
					, StringSplitOptions.RemoveEmptyEntries));
			}
		}

		public void LoadFromXmlNode(XmlNode e)
		{
            ID = e["series_mangadb_id"].InnerText != "" ? Int32.Parse(e["series_mangadb_id"].InnerText) : 0;
            Title = e["series_title"].InnerText;
            Synonyms = e["series_synonyms"].InnerText.Split(new string[] { "; " }, StringSplitOptions.RemoveEmptyEntries);
			Type = e ["series_type"].InnerText != "" ? (TypeManga)Int32.Parse(e ["series_type"].InnerText) : TypeManga.None;
            Chapters = e["series_chapters"].InnerText != "" ? Int32.Parse(e["series_chapters"].InnerText) : 0;
            Volumes = e["series_volumes"].InnerText != "" ? Int32.Parse(e["series_volumes"].InnerText) : 0;
			Status = e ["series_status"].InnerText != "" ? (PublishingStatus)Int32.Parse(e ["series_status"].InnerText) : PublishingStatus.None;
            PublishingStart = StringToDate(e["series_start"].InnerText);
            PublishingEnd = StringToDate(e["series_end"].InnerText);
			ImageUrl = e["series_image"].InnerText;
			MyID = e["my_id"].InnerText != "" ? Int32.Parse(e["my_id"].InnerText) : 0;
            MyReadChapters = e["my_read_chapters"].InnerText != "" ? Int32.Parse(e["my_read_chapters"].InnerText) : 0;
            MyReadVolumes = e["my_read_volumes"].InnerText != "" ? Int32.Parse(e["my_read_volumes"].InnerText) : 0;
			MyStartDate = StringToDate(e["my_start_date"].InnerText);
            MyFinishDate = StringToDate(e["my_finish_date"].InnerText);
			MyScore = e["my_score"].InnerText != "" ? Int32.Parse(e["my_score"].InnerText) : 0;
            MyReadingStatus = e["my_status"].InnerText != "" ? (ReadingStatus)Int32.Parse(e["my_status"].InnerText) : ReadingStatus.None;
            MyRereading = e["my_rereadingg"].InnerText != "" ? Int32.Parse(e["my_rereadingg"].InnerText) : 0;
            MyRereadingChapters = e["my_rereading_chap"].InnerText != "" ? Int32.Parse(e["my_rereading_chap"].InnerText) : 0;
			MyTags = e["my_tags"].InnerText;
		}

        public DateTime StringToDate(string date)
        {
            if (date != "0000-00-00" && date != "")
            {
                try
                {
                    return DateTime.ParseExact(date, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                }
                catch (FormatException)
                {
                    try
                    {
                        date = date.Substring(0, 7);
                        return DateTime.ParseExact(date, "yyyy-MM", CultureInfo.InvariantCulture);
                    }
                    catch (FormatException)
                    {
                        try
                        {
                            date = date.Substring(0, 4);
                            return DateTime.ParseExact(date, "yyyy", CultureInfo.InvariantCulture);
                        }
                        catch (FormatException)
                        {
                            return DateTime.MinValue;
                        }
                    }
                }
            }
            else
                return DateTime.MinValue;
        }

        public override string ToString()
        {
            return Title;
        }
    }
}
