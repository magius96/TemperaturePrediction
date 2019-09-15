using System.Collections.Generic;
using System.Globalization;
using NUnit.Framework;
using TemperaturePrediction;

namespace TemperaturePrediction_Tests
{
    [TestFixture]
    public class PredictionEngineTests
    {
        private List<PrecipitationData> SetupDataA()
        {
            var data = new List<PrecipitationData>();
            AddData("08/02/2012", 0.05f, ref data);
            AddData("08/02/2012", 0.1f, ref data);
            AddData("08/02/2012", 0.08f, ref data);
            AddData("08/03/2012", 0.96f, ref data);
            AddData("08/04/2012", 0f, ref data);
            AddData("08/05/2012", 0.03f, ref data);
            return data;
        }

        private List<PrecipitationData> SetupDataB()
        {
            var data = new List<PrecipitationData>();
            AddData("08/02/2012", 0f, ref data);
            AddData("08/02/2012", 0f, ref data);
            AddData("08/02/2012", 0f, ref data);
            AddData("08/03/2012", 0f, ref data);
            AddData("08/04/2012", 0f, ref data);
            AddData("08/05/2012", 0f, ref data);
            return data;
        }

        private void AddData(string dt, float value, ref List<PrecipitationData> data)
        {
            var pd = new PrecipitationData
            {
                Station = "TestStation",
                DateString = dt,
                PrecipitationString = value.ToString(CultureInfo.InvariantCulture)
            };
            data.Add(pd);
        }

        [Test]
        public void CleanupData_RemovesTwoEntriesForTestA()
        {
            var data = SetupDataA();
            var engine = new PredictionEngine(data);

            Assert.AreEqual(4, engine.Data.Count);
        }

        [Test]
        public void CleanupData_RemovesTwoEntriesForTestB()
        {
            var data = SetupDataB();
            var engine = new PredictionEngine(data);

            Assert.AreEqual(4, engine.Data.Count);
        }

        [Test]
        public void GetPrediction_TestA()
        {
            var data = SetupDataA();
            var engine = new PredictionEngine(data);
            var actual = engine.GetPrediction();

            Assert.AreEqual(0.27, actual);
        }

        [Test]
        public void GetPrediction_TestB()
        {
            var data = SetupDataB();
            var engine = new PredictionEngine(data);
            var actual = engine.GetPrediction();

            Assert.AreEqual(0, actual);
        }

        [Test]
        public void TestDataA_HasSixEntries()
        {
            var data = SetupDataA();

            Assert.AreEqual(6, data.Count);
        }

        [Test]
        public void TestDataB_HasSixEntries()
        {
            var data = SetupDataB();

            Assert.AreEqual(6, data.Count);
        }
    }
}