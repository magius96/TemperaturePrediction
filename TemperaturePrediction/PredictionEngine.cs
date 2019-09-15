using System;
using System.Collections.Generic;
using System.Linq;

namespace TemperaturePrediction
{
    /// <summary>
    ///     The PredictionEngine is used to calculate a precipitation prediction based on prior data
    /// </summary>
    public class PredictionEngine
    {
        /// <summary>
        ///     <para>The data that the prediction engine uses in its calculations.</para>
        ///     <para>Multiple entries on the same date are removed for this collection.</para>
        /// </summary>
        public readonly List<double> Data;

        /// <summary>
        ///     Constructor that sets the Data object while removing duplicate dates.
        /// </summary>
        /// <param name="data">Takes a list of PrecipitationData</param>
        public PredictionEngine(List<PrecipitationData> data)
        {
            Data = CleanupData(data);
        }

        /// <summary>
        ///     Attempts to predict the precipitation based on the data provided
        /// </summary>
        /// <returns>A double representing the amount of precipitation expected</returns>
        public double GetPrediction()
        {
            double prediction;
            if (Math.Abs(Data.Sum()) < double.Epsilon)
                prediction = 0;
            else
                prediction = Data.Average();

            return Math.Round(prediction, 2);
        }

        /// <summary>
        ///     Remove duplicate dates by dropping the lowest values
        /// </summary>
        /// <param name="data">The data to work through</param>
        /// <returns>Returns a list of floats</returns>
        private static List<double> CleanupData(List<PrecipitationData> data)
        {
            var values = new Dictionary<DateTime, double>();

            foreach (var pd in data)
                if (pd.Precipitation > float.MinValue)
                {
                    if (!values.ContainsKey(pd.Date))
                    {
                        values.Add(pd.Date, pd.Precipitation);
                    }
                    else
                    {
                        if (values[pd.Date] < pd.Precipitation) values[pd.Date] = pd.Precipitation;
                    }
                }

            var cleanResults = new List<double>();
            foreach (var kvp in values) cleanResults.Add(kvp.Value);

            return cleanResults;
        }
    }
}