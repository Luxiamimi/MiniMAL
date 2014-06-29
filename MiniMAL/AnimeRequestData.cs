using System;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using MiniMAL.Internal;

namespace MiniMAL
{
    //<?xml version="1.0" encoding="UTF-8"?>
    //<entry>
    //    <episode>11</episode>
    //    <status>1</status>
    //    <score>7</score>
    //    <downloaded_episodes></downloaded_episodes>
    //    <storage_type></storage_type>
    //    <storage_value></storage_value>
    //    <times_rewatched></times_rewatched>
    //    <rewatch_value></rewatch_value>
    //    <date_start></date_start>
    //    <date_finish></date_finish>
    //    <priority></priority>
    //    <enable_discussion></enable_discussion>
    //    <enable_rewatching></enable_rewatching>
    //    <comments></comments>
    //    <fansub_group></fansub_group>
    //    <tags>test tag, 2nd tag</tags>
    //</entry>

    [XmlRoot(ElementName = "entry", Namespace = "")]
    public class AnimeRequestData : EntryRequestData<WatchingStatus>
    {
        [XmlElement(ElementName = "episode")]
        public int? Episode { get; set; }
        [XmlElement(ElementName = "downloaded_episodes")]
        public int? DownloadedEpisodes { get; set; }
        [XmlElement(ElementName = "storage_type")]
        public int? StorageType { get; set; }
        [XmlElement(ElementName = "storage_value")]
        public float? StorageValue { get; set; }
        [XmlElement(ElementName = "times_rewatched")]
        public int? TimesRewatched { get; set; }
        [XmlElement(ElementName = "rewatch_value")]
        public int? RewatchValue { get; set; }
        [XmlElement(ElementName = "enable_rewatching")]
        public int? EnableRewatching { get; set; }
        [XmlElement(ElementName = "fansub_group")]
        public string FansubGroup { get; set; }

        public AnimeRequestData()
        {
        }

        public AnimeRequestData(Anime a)
        {
            Episode = a.MyWatchedEpisodes;
            Status = (int)a.MyStatus;
            Score = a.MyScore;
            DateStart = a.MyStartDate;
            DateFinish = a.MyEndDate;
            Tags = a.MyTags;
        }

        public static AnimeRequestData DefaultAddRequest(WatchingStatus status)
        {
            AnimeRequestData result = new AnimeRequestData();
            result.Status = (int)status;
            result.Episode = 1;
            result.Score = 0;
            result.DateStart = DateTime.Now;
            return result;
        }

        public override string SerializeToString()
        {
            CustomStringWriter result = new CustomStringWriter(new UTF8Encoding());
            CustomXmlWriter xml = new CustomXmlWriter(result);
            XmlSerializer serializer = new XmlSerializer(this.GetType());
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("", "");
            serializer.Serialize(xml, this, ns);

            return result.ToString().Replace("d2p1:nil=\"true\" xmlns:d2p1=\"http://www.w3.org/2001/XMLSchema-instance\"", "");
        }
    }
}