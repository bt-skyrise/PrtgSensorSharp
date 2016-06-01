using System;
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
            catch (Exception)
            {
                return PrtgReport.Failed("Sensor has failed - unhandled exception was thrown.");
            }
        }
    }
}