using System;
using System.Globalization;
using System.Xml;

namespace MiniMAL.Internal
{
    static internal class MALConverter
    {
        static public string XmlToString(XmlElement xml)
        {
            return xml.InnerText;
        }

        static public int XmlToInt(XmlElement xml)
        {
            return xml.InnerText != "" ? Int32.Parse(xml.InnerText) : 0;
        }

        static public double XmlToDouble(XmlElement xml)
        {
            return xml.InnerText != ""
                       ? Double.Parse(xml.InnerText, CultureInfo.InvariantCulture)
                       : 0;
        }

        static public DateTime XmlToDate(XmlElement xml)
        {
            string date = xml.InnerText;
            if (date == "0000-00-00" || date == "")
                return DateTime.MinValue;
            try
            {
                return DateTime.ParseExact(date, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            }
            catch (FormatException)
            {
                try
                {
                    date = date.Substring(0, 7);
                    return DateTime.ParseExact(date, "yyyy-MM", CultureInfo.InvariantCulture);
                }
                catch (FormatException)
                {
                    try
                    {
                        date = date.Substring(0, 4);
                        return DateTime.ParseExact(date, "yyyy", CultureInfo.InvariantCulture);
                    }
                    catch (FormatException)
                    {
                        return DateTime.MinValue;
                    }
                }
            }
        }
    }
}