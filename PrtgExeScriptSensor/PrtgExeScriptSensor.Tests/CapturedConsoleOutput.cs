using System;
using System.IO;

namespace PrtgExeScriptSensor.Tests
{
    public class CapturedConsoleOutput : IDisposable
    {
        public static CapturedConsoleOutput StartCapturing()
        {
            return new CapturedConsoleOutput();
        }

        private readonly StringWriter _stringWriter = new StringWriter();

        private CapturedConsoleOutput()
        {
            Console.SetOut(_stringWriter);
        }

        public string ReadAll() => _stringWriter.ToString();

        public void Dispose()
        {
            _stringWriter.Dispose();

            RestoreStandardOutput();
        }

        private static void RestoreStandardOutput()
        {
            var originalOutput = Console.OpenStandardOutput();

            Console.SetOut(new StreamWriter(originalOutput)
            {
                AutoFlush = true
            });
        }
    }
}