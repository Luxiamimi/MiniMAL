using System;
using System.Globalization;
using System.Xml.Serialization;

namespace MiniMAL
{
    public struct MiniMALDate : IXmlSerializable
    {
        public DateTime Data { get; set; }

        public MiniMALDate(DateTime dateTime) : this()
        {
            Data = dateTime;
        }

        public static implicit operator DateTime(MiniMALDate x)
        {
            return x.Data;
        }

        public static implicit operator MiniMALDate(DateTime x)
        {
            return new MiniMALDate(x);
        }

        public System.Xml.Schema.XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(System.Xml.XmlReader reader)
        {
            DateTime.ParseExact(reader.Value, "MMddyyyy", CultureInfo.CreateSpecificCulture("en"));
        }

        public void WriteXml(System.Xml.XmlWriter writer)
        {
            writer.WriteValue(Data.ToString("MMddyyyy"));
        }
    }
}
