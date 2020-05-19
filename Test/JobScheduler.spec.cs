using Test.Factories;
using NUnit.Framework;
using System;
using System.Collections.Generic;
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

            var jobSchedulerArray = jobScheduler.ToArray();
            var expectedSchedulerArray = new int[1][];
            expectedSchedulerArray[0] = new int[1];
            expectedSchedulerArray[0][0] = 1;

            var queue = new Queue<Job>();
            queue.Enqueue(jobObj);
            var expectedQueues = new List<Queue<Job>>
            {
                queue
            };

            Assert.IsNotEmpty(jobSchedulerArray);
            Assert.AreEqual(expectedSchedulerArray, jobSchedulerArray);
            Assert.AreEqual(expectedQueues, jobScheduler.GetQueues());
        }

        [Test]
        public void SchedulesTwoJobsInsideExecutionWindowWithProperEstimatedTime()
        {
            var date1 = DateFactory.Build("2019-11-10 09:00:00");
            var date2 = DateFactory.Build("2019-11-11 12:00:00");

            var executionWindow = new ExecutionWindow(date1, date2);
            var maxEstimatedTime = new EstimatedTimeBR("8 horas");
            var jobScheduler = new JobScheduler(executionWindow, maxEstimatedTime);

            var estimatedTime = new EstimatedTimeBR("2 horas");
            var jobObj = new Job(1, "Integration", Convert.ToDateTime("2019-11-10 12:00:00"), estimatedTime);
            jobScheduler.Schedule(jobObj);
            var estimatedTime2 = new EstimatedTimeBR("6 horas");
            var jobObj2 = new Job(2, "Integration2", Convert.ToDateTime("2019-11-10 14:00:00"), estimatedTime2);
            jobScheduler.Schedule(jobObj2);

            var jobSchedulerArray = jobScheduler.ToArray();
            var expectedSchedulerArray = new int[1][];
            expectedSchedulerArray[0] = new int[2];
            expectedSchedulerArray[0][0] = 1;
            expectedSchedulerArray[0][1] = 2;

            var queue = new Queue<Job>();
            queue.Enqueue(jobObj);
            queue.Enqueue(jobObj2);
            var expectedQueues = new List<Queue<Job>>
            {
                queue
            };

            Assert.IsNotEmpty(jobSchedulerArray);
            Assert.AreEqual(expectedSchedulerArray, jobSchedulerArray);
            Assert.AreEqual(expectedQueues, jobScheduler.GetQueues());
        }

        [Test]
        public void SchedulesThreeJobsInsideExecutionWindowWithProperEstimatedTime()
        {
            var date1 = DateFactory.Build("2019-11-10 09:00:00");
            var date2 = DateFactory.Build("2019-11-11 12:00:00");

            var executionWindow = new ExecutionWindow(date1, date2);
            var maxEstimatedTime = new EstimatedTimeBR("8 horas");
            var jobScheduler = new JobScheduler(executionWindow, maxEstimatedTime);

            var estimatedTime = new EstimatedTimeBR("2 horas");
            var jobObj = new Job(1, "Integration", Convert.ToDateTime("2019-11-10 12:00:00"), estimatedTime);
            jobScheduler.Schedule(jobObj);
            var estimatedTime2 = new EstimatedTimeBR("4 horas");
            var jobObj2 = new Job(2, "Integration2", Convert.ToDateTime("2019-11-11 12:00:00"), estimatedTime2);
            jobScheduler.Schedule(jobObj2);
            var estimatedTime3 = new EstimatedTimeBR("6 horas");
            var jobObj3 = new Job(3, "Integration3", Convert.ToDateTime("2019-11-11 08:00:00"), estimatedTime3);
            jobScheduler.Schedule(jobObj3);

            var jobSchedulerArray = jobScheduler.ToArray();
            var expectedSchedulerArray = new int[2][];
            expectedSchedulerArray[0] = new int[2];
            expectedSchedulerArray[0][0] = 1;
            expectedSchedulerArray[0][1] = 3;
            expectedSchedulerArray[1] = new int[1];
            expectedSchedulerArray[1][0] = 2;

            var queue = new Queue<Job>();
            queue.Enqueue(jobObj);
            queue.Enqueue(jobObj3);
            var queue2 = new Queue<Job>();
            queue2.Enqueue(jobObj2);

            var expectedQueues = new List<Queue<Job>>
            {
                queue,
                queue2
            };

            Assert.IsNotEmpty(jobSchedulerArray);
            Assert.AreEqual(expectedSchedulerArray, jobSchedulerArray);
            Assert.AreEqual(expectedQueues, jobScheduler.GetQueues());
        }

        [Test]
        public void SchedulesThreeJobsInsideExecutionWindowWithProperEstimatedTime2()
        {
            var date1 = DateFactory.Build("2019-11-10 09:00:00");
            var date2 = DateFactory.Build("2019-11-11 12:00:00");

            var executionWindow = new ExecutionWindow(date1, date2);
            var maxEstimatedTime = new EstimatedTimeBR("8 horas");
            var jobScheduler = new JobScheduler(executionWindow, maxEstimatedTime);

            var estimatedTime = new EstimatedTimeBR("6 horas");
            var jobObj = new Job(1, "Integration", Convert.ToDateTime("2019-11-10 12:00:00"), estimatedTime);
            jobScheduler.Schedule(jobObj);
            var estimatedTime2 = new EstimatedTimeBR("4 horas");
            var jobObj2 = new Job(2, "Integration2", Convert.ToDateTime("2019-11-11 12:00:00"), estimatedTime2);
            jobScheduler.Schedule(jobObj2);
            var estimatedTime3 = new EstimatedTimeBR("4 horas");
            var jobObj3 = new Job(3, "Integration3", Convert.ToDateTime("2019-11-11 08:00:00"), estimatedTime3);
            jobScheduler.Schedule(jobObj3);

            var jobSchedulerArray = jobScheduler.ToArray();
            var expectedSchedulerArray = new int[2][];
            expectedSchedulerArray[0] = new int[2];
            expectedSchedulerArray[0][0] = 2;
            expectedSchedulerArray[0][1] = 3;
            expectedSchedulerArray[1] = new int[1];
            expectedSchedulerArray[1][0] = 1;

            var queue = new Queue<Job>();
            queue.Enqueue(jobObj2);
            queue.Enqueue(jobObj3);
            var queue2 = new Queue<Job>();
            queue2.Enqueue(jobObj);

            var expectedQueues = new List<Queue<Job>>
            {
                queue,
                queue2
            };

            Assert.IsNotEmpty(jobSchedulerArray);
            Assert.AreEqual(expectedSchedulerArray, jobSchedulerArray);
            Assert.AreEqual(expectedQueues, jobScheduler.GetQueues());
        }
    }
}