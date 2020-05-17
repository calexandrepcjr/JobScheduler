using System;
using System.Globalization;

namespace Test.Factories
{
    internal static class DateFactory
    {
        private static readonly string Format = "yyyy-MM-dd HH:mm:ss";
        public static DateTime Build(string date)
        {
            return DateTime.ParseExact(date, Format, CultureInfo.InvariantCulture);
        }
    }
}
