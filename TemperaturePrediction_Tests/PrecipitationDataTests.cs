using System;
using System.Text;
using NUnit.Framework;
using TemperaturePrediction;

namespace TemperaturePrediction_Tests
{
    [TestFixture]
    public class PrecipitationDataTests
    {
        [OneTimeSetUp]
        public void SetUpFixture()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        }

        [Test]
        public void ReadAll_GetsData()
        {
            // initial data workbook has 4492 records
            var data = PrecipitationData.ReadAll("data.xlsx");

            Assert.IsNotNull(data, "data != null");
            // Current version of data may have more results
            Assert.GreaterOrEqual(data.Count, 4492, "data.Count < 4492");
            Console.WriteLine("Has {0} results, expected at least 4492.", data.Count);
        }

        [Test]
        public void ReadAll_WithInvalidPath()
        {
            var data = PrecipitationData.ReadAll("invalid.xlsx");
            Assert.IsNull(data);
        }

        [Test]
        public void ReadForMonthDay_GetsData()
        {
            // initial data workbook has 9 entries for august 17th
            var data = PrecipitationData.ReadForMonthDay("data.xlsx", 8, 17);

            Assert.IsNotNull(data, "data != null");

            // Current version of data may have more results
            Assert.GreaterOrEqual(data.Count, 9, "data.Count < 9");
            Console.Write("Has {0} results, expected at least 9.", data.Count);
        }

        [Test]
        public void ReadForMonthDay_WithInvalidPath()
        {
            var data = PrecipitationData.ReadForMonthDay("invalid.xlsx", 8, 17);
            Assert.IsNull(data);
        }
    }
}