using System.Globalization;
using System;
using System.Text.RegularExpressions;

namespace Shared
{
    public static  class DateTool
    {
        public static bool IsValidDate(string dateString)
        {
            if (string.IsNullOrWhiteSpace(dateString))
                return false;

            DateTime tempDate;
            bool isValid = DateTime.TryParseExact(
                dateString,
                "yyyy/MM/dd",
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out tempDate);

            return isValid;
        }

        public static bool IsValidYearMonthFormat(string date)
        {
            if (string.IsNullOrWhiteSpace(date))
                return false;

            // Check format YYYY/MM using regex
            var regex = new Regex(@"^20\d{2}/(0[1-9]|1[0-2])$");
            return regex.IsMatch(date);
        }
    }
}
