using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace MiniMAL
{
    public struct MALTags : IXmlSerializable
    {
        private string[] _tags;

        private MALTags(string[] tags)
            : this()
        {
            _tags = tags;
        }

        public MALTags(string textToSplit)
            : this()
        {
            _tags = textToSplit.Split(',');
        }

        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            _tags = reader.Value.Split(',');
        }

        public void WriteXml(XmlWriter writer)
        {
            writer.WriteValue(ToString());
        }

        public override string ToString()
        {
            return string.Join(",", _tags);
        }

        static public implicit operator string(MALTags x)
        {
            return x.ToString();
        }

        static public implicit operator MALTags(string x)
        {
            return new MALTags(x);
        }

        static public implicit operator string[](MALTags x)
        {
            return x._tags;
        }

        static public implicit operator MALTags(string[] x)
        {
            return new MALTags(x);
        }
    }
}