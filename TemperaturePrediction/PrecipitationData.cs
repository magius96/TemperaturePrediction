using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using ExcelDataReader;

namespace TemperaturePrediction
{
    public class PrecipitationData
    {
        /// <summary>
        ///     Station Identifier
        /// </summary>
        public string Station { get; set; }

        /// <summary>
        ///     String representing the date of the precipitation reading
        /// </summary>
        public string DateString { get; set; }

        /// <summary>
        ///     String representing the amount of precipitation detected
        /// </summary>
        public string PrecipitationString { get; set; }

        /// <summary>
        ///     Date that the reading was recorded
        /// </summary>
        public DateTime Date => DateString.ToDateTime();

        /// <summary>
        ///     Precipitation value of the reading
        /// </summary>
        public double Precipitation => PrecipitationString.ToDouble();

        /// <summary>
        ///     Reads all the precipitation data from the excel file
        /// </summary>
        /// <param name="path">The path to the file</param>
        /// <returns>List of precipitation Data, or Null if an error occurs.</returns>
        public static List<PrecipitationData> ReadAll(string path)
        {
            var lst = new List<PrecipitationData>();
            if (!File.Exists(path))
            {
                AppLog.Error("Could not find the file: {0}", path);
                return null;
            }

            try
            {
                using (var stream = File.Open(path, FileMode.Open, FileAccess.Read))
                {
                    using (var reader = ExcelReaderFactory.CreateReader(stream))
                    {
                        var result = reader.AsDataSet();
                        var table = result.Tables["in"];
                        for (var i = 0; i < table.Rows.Count; i++)
                        {
                            var pd = FromRow(table.Rows[i]);
                            lst.Add(pd);
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                AppLog.Exception(exception, "Error reading data file.");
                lst = null;
            }

            return lst;
        }

        /// <summary>
        ///     Reads all the precipitation data from the excel file where the month and day match the request.
        /// </summary>
        /// <param name="path">The path to the file</param>
        /// <param name="month">The numeric month to read data for</param>
        /// <param name="day">The day to read data for</param>
        /// <returns>List of precipitation Data, or Null if an error occurs.</returns>
        public static List<PrecipitationData> ReadForMonthDay(string path, int month, int day)
        {
            var lst = new List<PrecipitationData>();
            if (!File.Exists(path))
            {
                AppLog.Error("Could not find the file: {0}", path);
                return null;
            }

            try
            {
                using (var stream = File.Open(path, FileMode.Open, FileAccess.Read))
                {
                    using (var reader = ExcelReaderFactory.CreateReader(stream))
                    {
                        var result = reader.AsDataSet();
                        var table = result.Tables["in"];
                        for (var i = 0; i < table.Rows.Count; i++)
                        {
                            // Just look at the date row to see if this record to be included
                            var dtString = table.Rows[i][5].ToString();
                            var dt = dtString.ToDateTime();
                            if (dt.Month != month || dt.Day != day) continue;
                            var pd = FromRow(table.Rows[i]);
                            lst.Add(pd);
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                AppLog.Exception(exception, "Error reading data file.");
                lst = null;
            }

            return lst;
        }

        /// <summary>
        ///     Builds a PrecipitationData object from a DataRow
        /// </summary>
        /// <param name="row">A DataRow object that represents a PrecipitationData</param>
        /// <returns>Returns a PrecipitationData object</returns>
        private static PrecipitationData FromRow(DataRow row)
        {
            var pd = new PrecipitationData
            {
                Station = row[0].ToString(),
                DateString = row[5].ToString(),
                PrecipitationString = row[6].ToString()
            };

            return pd;
        }
    }
}