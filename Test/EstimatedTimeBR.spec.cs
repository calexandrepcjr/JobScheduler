using JobLib;
using NUnit.Framework;
using System;

namespace Test
{
    public class EstimatedTimeBRSpec
    {
        [Test]
        public void CreatesInstanceWithInteger()
        {
            var integerValue = 5;
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
        public void ConvertsHourProperly()
        {
            var obj = new EstimatedTimeBR("1 hora");

            Assert.AreEqual(3600, obj.ToSeconds());
            Assert.AreEqual(60, obj.ToMinutes());
            Assert.AreEqual(1, obj.ToHours());
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
        public void ConvertsMinuteProperly()
        {
            var obj = new EstimatedTimeBR("1 minuto");

            Assert.AreEqual(60, obj.ToSeconds());
            Assert.AreEqual(1, obj.ToMinutes());
            Assert.AreEqual(0, obj.ToHours());
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
        public void ConvertsSecondProperly()
        {
            var obj = new EstimatedTimeBR("1 segundo");

            Assert.AreEqual(1, obj.ToSeconds());
            Assert.AreEqual(0, obj.ToMinutes());
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

        [Test]
        public void ComparesGreaterThan()
        {
            var obj1 = new EstimatedTimeBR("5 segundos");
            var obj2 = new EstimatedTimeBR("8 segundos");

            Assert.True(obj2.GreaterThan(obj1));
        }

        [Test]
        public void ComparesGreaterThanAndFailsWhenIsLess()
        {
            var obj1 = new EstimatedTimeBR("8 segundos");
            var obj2 = new EstimatedTimeBR("5 segundos");

            Assert.False(obj2.GreaterThan(obj1));
        }

        [Test]
        public void ComparesLessThan()
        {
            var obj1 = new EstimatedTimeBR("5 segundos");
            var obj2 = new EstimatedTimeBR("8 segundos");

            Assert.True(obj1.LessThan(obj2));
        }

        [Test]
        public void ComparesLessThanAndFailsWhenIsGreater()
        {
            var obj1 = new EstimatedTimeBR("9 segundos");
            var obj2 = new EstimatedTimeBR("2 segundos");

            Assert.False(obj1.LessThan(obj2));
        }

        [Test]
        public void ComparesGreaterThanOrEqualWhenIsEqual()
        {
            var obj1 = new EstimatedTimeBR("5 segundos");
            var obj2 = new EstimatedTimeBR("5 segundos");

            Assert.True(obj1.GreaterThanOrEqual(obj2));
        }

        [Test]
        public void ComparesGreaterThanOrEqualWhenIsGreater()
        {
            var obj1 = new EstimatedTimeBR("10 segundos");
            var obj2 = new EstimatedTimeBR("5 segundos");

            Assert.True(obj1.GreaterThanOrEqual(obj2));
        }

        [Test]
        public void ComparesGreaterThanOrEqualAndFailsWhenIsLess()
        {
            var obj1 = new EstimatedTimeBR("2 segundos");
            var obj2 = new EstimatedTimeBR("5 segundos");

            Assert.False(obj1.GreaterThanOrEqual(obj2));
        }
    }
}