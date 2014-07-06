using System;
using System.Globalization;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace MiniMAL
{
    public struct MALDate : IXmlSerializable
    {
        private DateTime _date;

        private MALDate(DateTime dateTime)
            : this()
        {
            _date = dateTime;
        }

        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            _date = DateTime.ParseExact(reader.Value, "MMddyyyy",
                CultureInfo.CreateSpecificCulture("en"));
        }

        public void WriteXml(XmlWriter writer)
        {
            writer.WriteValue(_date.ToString("MMddyyyy"));
        }

        public override string ToString()
        {
            return _date.ToString("MM-dd-yyyy");
        }

        static public implicit operator DateTime(MALDate x)
        {
            return x._date;
        }

        static public implicit operator MALDate(DateTime x)
        {
            return new MALDate(x);
        }
    }
}