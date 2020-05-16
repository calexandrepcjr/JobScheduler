using System;
using System.Collections.Generic;
using System.Text;

namespace JobScheduler
{
    public class ExecutionWindow
    {
        public readonly Tuple<DateTime, DateTime> window;

        public ExecutionWindow(DateTime date1, DateTime date2)
        {
            window = new Tuple<DateTime, DateTime>(date1, date2);
        }

        public ExecutionWindow(string date1, string date2)
        {
            window = new Tuple<DateTime, DateTime>(
                StringToDate(date1),
                StringToDate(date2)
                );
        }


        public bool IsIn(DateTime date)
        {
            return date >= window.Item1 && date <= window.Item2;
        }

        private DateTime StringToDate(string date)
        {
            return Convert.ToDateTime(date);
        }
    }
}
