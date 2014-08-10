using System.Xml.Serialization;

namespace MiniMAL.Types
{
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