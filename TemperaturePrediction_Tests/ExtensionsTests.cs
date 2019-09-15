using System;
using NUnit.Framework;
using TemperaturePrediction;

namespace TemperaturePrediction_Tests
{
    [TestFixture]
    public class ExtensionsTests
    {
        [Test]
        public void ToDateTime_WithInvalidString()
        {
            var testString = "some string";
            var expected = DateTime.MinValue;

            var actual = testString.ToDateTime();

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void ToDateTime_WithValidString()
        {
            var testString = "2019-04-16";
            var expected = new DateTime(2019, 4, 16);

            var actual = testString.ToDateTime();

            Assert.AreEqual(expected.ToShortDateString(), actual.ToShortDateString());
        }

        [Test]
        public void ToDouble_WithInvalidString()
        {
            var testString = "testing";
            var expected = double.MinValue;

            var actual = testString.ToDouble();

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void ToDouble_WithValidString()
        {
            var testString = "3.14159265359";
            var expected = 3.14159265359;

            var actual = testString.ToDouble();

            var diff = Math.Abs(expected - actual);
            Assert.Less(diff, double.Epsilon);
        }

        [Test]
        public void GetMonthName_January()
        {
            var dt = new DateTime(2012, 1, 15);
            var actual = dt.GetMonthName();

            StringAssert.AreEqualIgnoringCase("january", actual);
        }

        [Test]
        public void GetMonthName_December()
        {
            var dt = new DateTime(2012, 12, 15);
            var actual = dt.GetMonthName();

            StringAssert.AreEqualIgnoringCase("december", actual);
        }
    }
}