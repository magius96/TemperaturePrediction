using NUnit.Framework;
using TemperaturePrediction;

namespace TemperaturePrediction_Tests
{
    [TestFixture]
    public class ResultDataTests
    {
        [Test]
        public void ResultData_IncludesLogs()
        {
            AppLog.Log("Test Log 1");
            AppLog.Error("Test Error 1");
            var rd = new ResultData {EstimatedPrecipitation = 0.1};

            var json = rd.ToString();

            StringAssert.Contains("LOG: Test Log 1", json, "Doesn't contain test log 1");
            StringAssert.Contains("ERR: Test Error 1", json, "Doesn't contain test error 1");
        }

        [Test]
        public void ResultData_IncludesValue()
        {
            var rd = new ResultData {EstimatedPrecipitation = 0.123};

            var json = rd.ToString();

            StringAssert.Contains("\"EstimatedPrecipitation\": 0.12,", json);
        }
    }
}