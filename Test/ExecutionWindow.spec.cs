using JobLib;
using NUnit.Framework;
using System;
using Test.Factories;

namespace Test
{
    public class ExecutionWindowSpecs
    {
        [Test]
        public void CreatesInstanceWithTwoDateTime()
        {
            DateTime date1 = DateFactory.Build("2019-11-10 09:00:00");
            DateTime date2 = DateFactory.Build("2019-11-11 12:00:00");

            var executionObj = new ExecutionWindow(date1, date2);

            Assert.IsInstanceOf(typeof(ExecutionWindow), executionObj);
            Assert.AreEqual(date1, executionObj.Window.Item1);
            Assert.AreEqual(date2, executionObj.Window.Item2);
        }

        [Test]
        public void CreatesInstanceWithTwoDateTimeFromTheSmallestToLargest()
        {
            DateTime date1 = DateFactory.Build("2019-11-11 09:00:00");
            DateTime date2 = DateFactory.Build("2019-11-10 12:00:00");

            var executionObj = new ExecutionWindow(date1, date2);

            Assert.IsInstanceOf(typeof(ExecutionWindow), executionObj);
            Assert.AreEqual(date2, executionObj.Window.Item1);
            Assert.AreEqual(date1, executionObj.Window.Item2);
        }

        [Test]
        public void RespondsTrueToIsInWhenDateIsInsideExecutionWindow()
        {
            DateTime date1 = DateFactory.Build("2019-11-11 09:00:00");
            DateTime date2 = DateFactory.Build("2019-11-10 12:00:00");

            DateTime date3 = DateFactory.Build("2019-11-11 08:00:00");

            var executionObj = new ExecutionWindow(date1, date2);

            Assert.True(executionObj.IsIn(date3));
        }

        [Test]
        public void RespondsFalseToIsInWhenDateIsOutsideExecutionWindow()
        {
            DateTime date1 = DateFactory.Build("2019-11-11 09:00:00");
            DateTime date2 = DateFactory.Build("2019-11-10 12:00:00");

            DateTime date3 = DateFactory.Build("2019-11-20 10:00:00");

            var executionObj = new ExecutionWindow(date1, date2);

            Assert.False(executionObj.IsIn(date3));
        }
    }
}