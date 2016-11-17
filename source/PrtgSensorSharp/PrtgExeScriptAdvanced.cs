using System;
using System.Linq;
using System.Xml.Linq;

namespace PrtgSensorSharp
{
    public static class PrtgExeScriptAdvanced
    {
        public static void Run(Func<IPrtgReport> probe, bool printExceptionDetails = false)
        {
            var report = GenerateReport(probe, printExceptionDetails);

            var serializedReport = report.Serialize().ToString(SaveOptions.DisableFormatting);

            Console.Write(serializedReport);
        }

        private static IPrtgReport GenerateReport(Func<IPrtgReport> probe, bool printExceptionDetails)
        {
            try
            {
                return probe();
            }
            catch (Exception e)
            {
                return printExceptionDetails
                    ? GenerateReportFromExceptionMessage(e)
                    : PrtgReport.Failed("Sensor has failed - unhandled exception was thrown.");
            }
        }

        private static IPrtgReport GenerateReportFromExceptionMessage(Exception exception)
        {
            var aggregateException = exception as AggregateException;

            if (aggregateException != null)
            {
                var innerExceptionsMessages = ExtractInnerExceptionsMessages(aggregateException);

                return PrtgReport.Failed($"Sensor has failed - An aggregate exception '{exception.Message}' with following inner exceptions was thrown: {innerExceptionsMessages}");
            }

            return PrtgReport.Failed($"Sensor has failed - An exception was thrown: {exception.Message}");
        }

        private static string ExtractInnerExceptionsMessages(AggregateException aggregateException) => aggregateException
            .Flatten()
            .InnerExceptions
            .Aggregate("", (current, innerException) => $"{current}{innerException.Message}; ");
    }
}