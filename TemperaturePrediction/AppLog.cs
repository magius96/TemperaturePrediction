using System;
using System.Collections.Generic;

namespace TemperaturePrediction
{
    public static class AppLog
    {
        private static readonly List<string> Messages = new List<string>();

        public static void Debug(string format, params object[] args)
        {
            HandleLog("DBG", format, args);
        }

        public static void Log(string format, params object[] args)
        {
            HandleLog("LOG", format, args);
        }

        public static void Error(string format, params object[] args)
        {
            HandleLog("ERR", format, args);
        }

        private static void HandleLog(string key, string format, params object[] args)
        {
            var message = FormatMessage(format, args);
            if (!string.IsNullOrWhiteSpace(message)) Messages.Add(string.Format("{0}: {1}", key, message));
        }

        public static void Exception(Exception exception, string format = "", params object[] args)
        {
            var message = FormatMessage(format, args);
            if (!string.IsNullOrWhiteSpace(message))
                Messages.Add(string.Format("EXC: {0} :: {1}", message, exception.Message));
        }

        public static List<string> GetMessages()
        {
            return Messages;
        }

        private static string FormatMessage(string format, params object[] args)
        {
            var message = "";
            if (!string.IsNullOrWhiteSpace(format))
                try
                {
                    message = string.Format(format, args);
                }
                catch
                {
                    Error("Invalid error message format: {0}", format.Replace("{", "(").Replace("}", ")"));
                    message = "";
                }

            return message;
        }
    }
}