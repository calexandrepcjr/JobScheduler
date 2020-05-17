using Test.Factories;
using NUnit.Framework;
using System;
using JobLib;

namespace Test
{
    public class JobSchedulerSpec
    {
        [Test]
        public void CreatesInstanceWithExecutionWindowAndEstimatedTime()
        {
            var date1 = DateFactory.Build("2019-11-10 09:00:00");
            var date2 = DateFactory.Build("2019-11-11 12:00:00");

            var executionWindow = new ExecutionWindow(date1, date2);
            var maxEstimatedTime = new EstimatedTimeBR("8 horas");
            var jobScheduler = new JobScheduler(executionWindow, maxEstimatedTime);

            Assert.IsInstanceOf(typeof(JobScheduler), jobScheduler);
        }

        [Test]
        public void RespondsToMaxJobDuration()
        {
            var date1 = DateFactory.Build("2019-11-10 09:00:00");
            var date2 = DateFactory.Build("2019-11-11 12:00:00");

            var executionWindow = new ExecutionWindow(date1, date2);
            var maxEstimatedTime = new EstimatedTimeBR("8 horas");
            var jobScheduler = new JobScheduler(executionWindow, maxEstimatedTime);

            Assert.IsInstanceOf(typeof(JobScheduler), jobScheduler);
            Assert.AreEqual(maxEstimatedTime, jobScheduler.MaxJobDuration());
        }

        [Test]
        public void ItDoesNotSchedulesAJobAboveEstimatedTimeThreshold()
        {
            var date1 = DateFactory.Build("2019-11-10 09:00:00");
            var date2 = DateFactory.Build("2019-11-11 12:00:00");

            var executionWindow = new ExecutionWindow(date1, date2);
            var maxEstimatedTime = new EstimatedTimeBR("8 horas");
            var jobScheduler = new JobScheduler(executionWindow, maxEstimatedTime);

            var estimatedTime = new EstimatedTimeBR("10 horas");
            var jobObj = new Job(1, "Integration", new DateTime(), estimatedTime);
            jobScheduler.Schedule(jobObj);

            Assert.IsEmpty(jobScheduler.ToArray());
        }

        [Test]
        public void ItDoesNotSchedulesAJobAboveExecutionWindow()
        {
            var date1 = DateFactory.Build("2019-11-10 09:00:00");
            var date2 = DateFactory.Build("2019-11-11 12:00:00");

            var executionWindow = new ExecutionWindow(date1, date2);
            var maxEstimatedTime = new EstimatedTimeBR("8 horas");
            var jobScheduler = new JobScheduler(executionWindow, maxEstimatedTime);

            var estimatedTime = new EstimatedTimeBR("4 horas");
            var jobObj = new Job(1, "Integration", Convert.ToDateTime("2019-11-15 12:00:00"), estimatedTime);
            jobScheduler.Schedule(jobObj);

            Assert.IsEmpty(jobScheduler.ToArray());
        }

        [Test]
        public void SchedulesAJobInsideExecutionWindowWithProperEstimatedTime()
        {
            var date1 = DateFactory.Build("2019-11-10 09:00:00");
            var date2 = DateFactory.Build("2019-11-11 12:00:00");

            var executionWindow = new ExecutionWindow(date1, date2);
            var maxEstimatedTime = new EstimatedTimeBR("8 horas");
            var jobScheduler = new JobScheduler(executionWindow, maxEstimatedTime);

            var estimatedTime = new EstimatedTimeBR("2 horas");
            var jobObj = new Job(1, "Integration", Convert.ToDateTime("2019-11-10 12:00:00"), estimatedTime);
            jobScheduler.Schedule(jobObj);

            Assert.IsEmpty(jobScheduler.ToArray());
        }
    }
}