using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace TemperaturePrediction
{
    /// <summary>
    ///     Represents the results of a prediction calculation
    /// </summary>
    public class ResultData
    {
        private double _estimate;

        /// <summary>
        ///     Initializes the estimated precipitation to zero and instantiates the LogMessages list.
        /// </summary>
        public ResultData()
        {
            EstimatedPrecipitation = 0.0;
            LogMessages = new List<string>();
        }

        /// <summary>
        ///     The float value that results from a prediction calculation
        /// </summary>
        public double EstimatedPrecipitation
        {
            get => _estimate;
            set => _estimate = Math.Round(value, 2);
        }

        /// <summary>
        ///     A list of all log messages that occur during processing
        /// </summary>
        public List<string> LogMessages { get; set; }

        /// <summary>
        ///     Override: Returns the ResultData as a JSON string, including all current log messages
        /// </summary>
        /// <returns>Returns a JSON string that represents the ResultData including all log messages.</returns>
        public override string ToString()
        {
            LogMessages = AppLog.GetMessages();
            var json = JsonConvert.SerializeObject(this, Formatting.Indented);
            return json;
        }
    }
}