using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    public static class DateTimeExtensions
    {



        public static string ToPersian(this DateTime dateTime)
        {
            PersianCalendar persianCalendar = new PersianCalendar();
            int year = persianCalendar.GetYear(dateTime);
            int month = persianCalendar.GetMonth(dateTime);
            int day = persianCalendar.GetDayOfMonth(dateTime);
            return $"{year}-{month:D2}-{day:D2}"; // فرمت YYYY/MM/DD  
        }

        public static DateTime ToMiladi(this string persianDate)
        {
            if (string.IsNullOrWhiteSpace(persianDate))
            {
                throw new ArgumentException("Input date string cannot be null or empty.");
            }

            var parts = persianDate.Split('-');
            if (parts.Length != 3)
            {
                throw new FormatException("Invalid date format. Expected format is YYYY/MM/DD.");
            }

            int year = int.Parse(parts[0]);
            int month = int.Parse(parts[1]);
            int day = int.Parse(parts[2]);

            PersianCalendar persianCalendar = new PersianCalendar();

            DateTime miladiDateTime = persianCalendar.ToDateTime(year, month, day, 0, 0, 0, 0);
            return miladiDateTime;
        }
        public static int ToPersianYear(this DateTime dateTime)
        {
            PersianCalendar persianCalendar = new PersianCalendar();
            return persianCalendar.GetYear(dateTime); // فقط سال را برمی‌گرداند  
        }
    }
}
