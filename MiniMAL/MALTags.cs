using System.Xml.Serialization;

namespace MiniMAL
{
    public struct MALTags : IXmlSerializable
    {
        private string[] _tags;

        public MALTags(string[] tags)
            : this()
        {
            _tags = tags;
        }

        public MALTags(string textToSplit)
            : this()
        {
            _tags = textToSplit.Split(',');
        }

        public override string ToString()
        {
            return string.Join(",", _tags);
        }

        public static implicit operator string(MALTags x)
        {
            return x.ToString();
        }

        public static implicit operator MALTags(string x)
        {
            return new MALTags(x);
        }

        public static implicit operator string[](MALTags x)
        {
            return x._tags;
        }

        public static implicit operator MALTags(string[] x)
        {
            return new MALTags(x);
        }

        public System.Xml.Schema.XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(System.Xml.XmlReader reader)
        {
            _tags = reader.Value.Split(',');
        }

        public void WriteXml(System.Xml.XmlWriter writer)
        {
            writer.WriteValue(ToString());
        }
    }
}
