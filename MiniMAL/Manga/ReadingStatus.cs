using System.Xml.Serialization;

namespace MiniMAL.Manga
{
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
}