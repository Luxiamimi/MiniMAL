using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace MiniMAL
{
    internal class MiniMALTools
    {
        public static DateTime StringToDate(string date)
        {
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
