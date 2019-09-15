using System;

namespace TemperaturePrediction
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var prog = new Program();
            // if no date, or an invalid date, then use today's date.
            if (args.Length > 0)
            {
                var dt = args[0].ToDateTime();
                if (dt == DateTime.MinValue)
                {
                    AppLog.Error("Could not parse the date, using today's date.", args[0]);
                    dt = DateTime.Today;
                }

                prog.Start(dt);
            }
            else
            {
                AppLog.Log("No date received, using today's date.");
                prog.Start(DateTime.Today);
            }

            Pause();
        }

        private static void Pause()
        {
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Press any key to continue  . . .");
            Console.ReadKey(true);
        }

        private void Start(DateTime date)
        {
            AppLog.Log("Processing for date: {0}", date.ToShortDateString());

            var data = PrecipitationData.ReadForMonthDay("data.xlsx", date.Month, date.Day);
            AppLog.Log("Found {0} entries in data file for {1} {2}", data.Count, date.GetMonthName(), date.Day);
            var engine = new PredictionEngine(data);
            var removed = data.Count - engine.Data.Count;
            AppLog.Log("Data Cleanup removed {0} duplicate date entries.", removed);
            var res = new ResultData {EstimatedPrecipitation = engine.GetPrediction()};

            Console.WriteLine(res.ToString());
        }
    }
}