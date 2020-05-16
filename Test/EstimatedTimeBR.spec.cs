using JobScheduler;
using NUnit.Framework;
using System;

namespace Test
{
    public class EstimatedTimeBRSpec
    {
        [Test]
        public void CreatesInstanceWithInteger()
        {
            int integerValue = 5;
            var obj = new EstimatedTimeBR(integerValue);

            Assert.IsInstanceOf(typeof(EstimatedTimeBR), obj);
            Assert.AreEqual(integerValue, obj.ToSeconds());
        }

        [Test]
        public void ThrowsWhenCreatesInstanceWithInvalidString()
        {
            Assert.Throws<ArgumentException>(() => new EstimatedTimeBR("horas"));
        }

        [Test]
        public void ThrowsWhenCreatesInstanceWithInvalidPeriod()
        {
            Assert.Throws<ArgumentException>(() => new EstimatedTimeBR("5 hoas"));
        }

        [Test]
        public void CreatesInstanceWithValidString()
        {
            var obj = new EstimatedTimeBR("5 segundos");

            Assert.IsInstanceOf(typeof(EstimatedTimeBR), obj);
        }

        [Test]
        public void ConvertsHoursProperly()
        {
            var obj = new EstimatedTimeBR("5 horas");

            Assert.AreEqual(18000, obj.ToSeconds());
            Assert.AreEqual(300, obj.ToMinutes());
            Assert.AreEqual(5, obj.ToHours());
        }

        [Test]
        public void ConvertsMinutesProperly()
        {
            var obj = new EstimatedTimeBR("5 minutos");

            Assert.AreEqual(300, obj.ToSeconds());
            Assert.AreEqual(5, obj.ToMinutes());
            Assert.AreEqual(0, obj.ToHours());
        }

        [Test]
        public void ConvertsSecondsProperly()
        {
            var obj = new EstimatedTimeBR("5 segundos");

            Assert.AreEqual(5, obj.ToSeconds());
            Assert.AreEqual(0, obj.ToMinutes());
            Assert.AreEqual(0, obj.ToHours());
        }
    }
}