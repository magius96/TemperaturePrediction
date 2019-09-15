using System.Globalization;
using System.Linq;
using System.Reflection;
using NUnit.Framework;
using TemperaturePrediction;

namespace TemperaturePrediction_Tests
{
    [TestFixture]
    public class AppLogTests
    {
        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            AppLog.Log("Test log 1");
            AppLog.Error("Test error 1");
            AppLog.Log("Test log 2");
            AppLog.Debug("Test debug 1");
            AppLog.Exception(new AmbiguousMatchException(), "Test exception 1");
            AppLog.Debug("Invalid Format {0}");
        }

        [Test]
        public void AppLog_ContainsInvalidFormatError()
        {
            var lst = AppLog.GetMessages();
            var count = lst.Count(i => i.Contains("Invalid error message format:"));

            Assert.AreEqual(1, count);
        }

        [Test]
        public void AppLog_HasOneDebug()
        {
            var lst = AppLog.GetMessages();
            var count = lst.Count(i => i.StartsWith("DBG", false, CultureInfo.CurrentCulture));

            Assert.AreEqual(1, count);
        }

        [Test]
        public void AppLog_HasOneException()
        {
            var lst = AppLog.GetMessages();
            var count = lst.Count(i => i.StartsWith("EXC", false, CultureInfo.CurrentCulture));

            Assert.AreEqual(1, count);
        }

        [Test]
        public void AppLog_HasSixMessages()
        {
            var lst = AppLog.GetMessages();

            Assert.AreEqual(6, lst.Count);
        }

        [Test]
        public void AppLog_HasTwoError()
        {
            var lst = AppLog.GetMessages();
            var count = lst.Count(i => i.StartsWith("ERR", false, CultureInfo.CurrentCulture));

            Assert.AreEqual(2, count);
        }

        [Test]
        public void AppLog_HasTwoLogMessages()
        {
            var lst = AppLog.GetMessages();
            var count = lst.Count(i => i.StartsWith("LOG", false, CultureInfo.CurrentCulture));

            Assert.AreEqual(2, count);
        }
    }
}