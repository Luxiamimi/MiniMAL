using System.Xml.Serialization;

namespace MiniMAL
{
    public enum TypeAnime
    {
        None = 0,
        TV = 1,
        OVA = 2,
        Movie = 3,
        Special = 4,
        ONA = 5
    }

    public enum AiringStatus
    {
        None = 0,
        Airing = 1,
        Finished = 2,
        NoYetAiring = 3
    }

    public enum WatchingStatus
    {
        None = 0,
        [XmlEnum("1")]
        Watching = 1,
        [XmlEnum("2")]
        Completed = 2,
        [XmlEnum("3")]
        OnHold = 3,
        [XmlEnum("4")]
        Dropped = 4,
        [XmlEnum("6")]
        PlanToWatch = 6
    }

    public enum TypeManga
    {
        None = 0,
        Manga = 1,
        Novel = 2,
        OneShot = 3,
        Doujin = 4,
        Manhwa = 5,
        Manhua = 6
    }

    public enum PublishingStatus
    {
        None = 0,
        Publishing = 1,
        Finished = 2,
        NoYetPublished = 3
    }

    public enum ReadingStatus
    {
        None = 0,
        [XmlEnum("1")]
        Reading = 1,
        [XmlEnum("2")]
        Completed = 2,
        [XmlEnum("3")]
        OnHold = 3,
        [XmlEnum("4")]
        Dropped = 4,
        [XmlEnum("6")]
        PlanToRead = 6
    }

    public enum Priority
    {
        [XmlEnum("0")]
        Low = 0,
        [XmlEnum("1")]
        Medium = 1,
        [XmlEnum("2")]
        High = 2
    }
}