using JobScheduler;
using NUnit.Framework;
using System;

namespace Test
{
    public class ExecutionWindowSpecs
    {
        [Test]
        public void CreatesInstanceWithTwoDateTime()
        {
            DateTime date1 = Convert.ToDateTime("2019-11-10 09:00:00");
            DateTime date2 = Convert.ToDateTime("2019-11-11 12:00:00");

            var executionObj = new ExecutionWindow(date1, date2);

            Assert.IsInstanceOf(typeof(ExecutionWindow), executionObj);
        }

        [Test]
        public void CreatesInstanceWithTwoDateString()
        {
            string date1 = "2019-11-10 09:00:00";
            string date2 = "2019-11-11 12:00:00";

            var executionObj = new ExecutionWindow(date1, date2);

            Assert.IsInstanceOf(typeof(ExecutionWindow), executionObj);
        }

        [Test]
        public void RespondsTrueToIsInWhenDateIsInsideExecutionWindow()
        {
            string date1 = "2019-11-10 09:00:00";
            string date2 = "2019-11-11 12:00:00";

            DateTime date3 = Convert.ToDateTime("2019-11-11 10:00:00");

            var executionObj = new ExecutionWindow(date1, date2);

            Assert.True(executionObj.IsIn(date3));
        }

        [Test]
        public void RespondsFalseToIsInWhenDateIsOutsideExecutionWindow()
        {
            string date1 = "2019-11-10 09:00:00";
            string date2 = "2019-11-11 12:00:00";

            DateTime date3 = Convert.ToDateTime("2019-11-20 10:00:00");

            var executionObj = new ExecutionWindow(date1, date2);

            Assert.False(executionObj.IsIn(date3));
        }
    }
}