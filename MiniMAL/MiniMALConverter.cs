using System;
using System.ComponentModel;
using System.Globalization;
using System.Xml;

namespace MiniMAL
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    internal class MiniMALConverter
    {
        public static int XmlToInt(XmlElement xml)
        {
            return xml.InnerText != "" ? Int32.Parse(xml.InnerText) : 0;
        }

        public static double XmlToDouble(XmlElement xml)
        {
            return xml.InnerText != "" ? Double.Parse(xml.InnerText, CultureInfo.InvariantCulture) : 0;
        }

        public static DateTime XmlToDate(XmlElement xml)
        {
            string date = xml.InnerText;
            if (date != "0000-00-00" && date != "")
            {
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
            else
                return DateTime.MinValue;
        }
    }
}