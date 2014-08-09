using System.Xml.Serialization;

namespace MiniMAL.Anime
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
}