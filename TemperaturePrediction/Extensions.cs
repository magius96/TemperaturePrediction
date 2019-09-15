using System;

namespace TemperaturePrediction
{
    public static class Extensions
    {
        /// <summary>
        ///     Attempts to parse a string into a DateTime value
        /// </summary>
        /// <param name="s">A string value to parse into a DateTime</param>
        /// <returns>
        ///     Returns a DateTime equivalent of the string,
        ///     or DateTime.MinValue if an error occurs.
        /// </returns>
        public static DateTime ToDateTime(this string s)
        {
            try
            {
                if (!DateTime.TryParse(s, out var dt)) dt = DateTime.MinValue;

                return dt;
            }
            catch (Exception exception)
            {
                AppLog.Exception(exception, "Could not parse string to DateTime variable: {0}", s);
                return DateTime.MinValue;
            }
        }

        /// <summary>
        ///     Attempts to parse a string into a double value
        /// </summary>
        /// <param name="s">A string value to parse into a double</param>
        /// <returns>
        ///     Returns a double equivalent of the string,
        ///     or double.MinValue if an error occurs.
        /// </returns>
        public static double ToDouble(this string s)
        {
            try
            {
                if (!double.TryParse(s, out var f)) f = double.MinValue;

                return f;
            }
            catch (Exception exception)
            {
                AppLog.Exception(exception, "Could not parse string to double variable: {0}", s);
                return double.MinValue;
            }
        }

        /// <summary>
        /// Gets the month name from the date.
        /// </summary>
        /// <param name="dt">A DateTime value</param>
        /// <returns>A string representing the name of the month.</returns>
        public static string GetMonthName(this DateTime dt)
        {
            var month = dt.Month;
            switch (month)
            {
                case 1: return "January";
                case 2: return "February";
                case 3: return "March";
                case 4: return "April";
                case 5: return "May";
                case 6: return "June";
                case 7: return "July";
                case 8: return "August";
                case 9: return "September";
                case 10: return "October";
                case 11: return "November";
                case 12: return "December";
                default: return "Unknown Month";
            }
        }
    }
}