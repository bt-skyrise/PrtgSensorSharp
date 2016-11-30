using System;
using System.Linq;
using System.Xml.Linq;

namespace PrtgSensorSharp
{
    public static class PrtgExeScriptAdvanced
    {
        public static void Run(Func<IPrtgReport> probe)
        {
            var report = GenerateReport(probe);

            var serializedReport = report.Serialize().ToString(SaveOptions.DisableFormatting);

            Console.Write(serializedReport);
        }

        private static IPrtgReport GenerateReport(Func<IPrtgReport> probe)
        {
            try
            {
                return probe();
            }
            catch (AggregateException exception)
            {
                var innerExceptionsMessages = ExtractInnerExceptionsMessages(exception);

                return PrtgReport.Failed($"Sensor has failed - an aggregate exception '{exception.Message}' " +
                                         $"with following inner exceptions was thrown: {innerExceptionsMessages}");
            }
            catch (Exception exception)
            {
                return PrtgReport.Failed($"Sensor has failed - an exception was thrown: {exception.Message}");
            }
        }

        private static string ExtractInnerExceptionsMessages(AggregateException aggregateException)
        {
            var innerExceptionMessages = aggregateException
                .Flatten()
                .InnerExceptions
                .Select(exception => exception.Message);

            return string.Join("; ", innerExceptionMessages);
        }
    }
}