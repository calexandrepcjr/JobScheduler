using System;
using System.Globalization;
using Example.Factories;

namespace Test.Factories
{
    public class DateFactory : BaseFactory<DateTime>
    {
        private static readonly string Format = "yyyy-MM-dd HH:mm:ss";
        private readonly string Date;

        public DateFactory(string date)
        {
            Date = date;
        }

        public override DateTime Build()
        {
            return DateTime.ParseExact(Date, Format, CultureInfo.InvariantCulture);
        }
    }
}
