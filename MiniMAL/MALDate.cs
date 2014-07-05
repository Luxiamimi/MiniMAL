using System;
using System.Globalization;
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

        public override string ToString()
        {
            return _date.ToString("MM-dd-yyyy");
        }

        public static implicit operator DateTime(MALDate x)
        {
            return x._date;
        }

        public static implicit operator MALDate(DateTime x)
        {
            return new MALDate(x);
        }

        public System.Xml.Schema.XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(System.Xml.XmlReader reader)
        {
            _date = DateTime.ParseExact(reader.Value, "MMddyyyy", CultureInfo.CreateSpecificCulture("en"));
        }

        public void WriteXml(System.Xml.XmlWriter writer)
        {
            writer.WriteValue(_date.ToString("MMddyyyy"));
        }
    }
}