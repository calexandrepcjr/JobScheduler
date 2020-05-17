using NUnit.Framework;
using System;
using JobLib;

namespace Test
{
    public class JobSpec
    {
        [Test]
        public void CreatesInstanceWithProperEstimation()
        {
            var estimatedTime = new EstimatedTimeBR("5 horas");
            var jobObj = new Job(1, "Integration", new DateTime(), estimatedTime);

            Assert.IsInstanceOf(typeof(Job), jobObj);
            Assert.AreEqual(18000, jobObj.Duration());
        }
    }
}