using System;

namespace JobLib
{
    public class ExecutionWindow
    {
        public readonly Tuple<DateTime, DateTime> Window;

        public ExecutionWindow(DateTime date1, DateTime date2)
        {
            if (date2 < date1)
            {
                Window = new Tuple<DateTime, DateTime>(date2, date1);

                return;
            }

            Window = new Tuple<DateTime, DateTime>(date1, date2);
        }


        public bool IsIn(DateTime date)
        {
            return date >= Window.Item1 && date <= Window.Item2;
        }
    }
}
